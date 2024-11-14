using Azure.Storage.Blobs;
using FileUploadApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class FileUploadController : ControllerBase
{
    private readonly FileDbContext _context;
    private readonly BlobServiceClient _blobServiceClient;

    public FileUploadController(FileDbContext context, BlobServiceClient blobServiceClient)
    {
        _context = context;
        _blobServiceClient = blobServiceClient;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        // Usa el nombre correcto del contenedor
        string containerName = "almacenamientoblob";
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        string blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var blobClient = containerClient.GetBlobClient(blobName);

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream);
        }

        var fileMetadata = new FileMetadata
        {
            FileName = file.FileName,
            FileType = file.ContentType,
            UploadDate = DateTime.UtcNow,
            BlobUrl = blobClient.Uri.ToString()
        };

        _context.Files.Add(fileMetadata);
        await _context.SaveChangesAsync();

        return Ok(new { fileMetadata.BlobUrl });
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteFile(int id)
    {
        var fileMetadata = await _context.Files.FindAsync(id);
        if (fileMetadata == null)
            return NotFound("File not found.");

        // Eliminar archivo de Azure Blob Storage
        var containerClient = _blobServiceClient.GetBlobContainerClient("almacenamientoblob");
        var blobClient = containerClient.GetBlobClient(fileMetadata.BlobUrl);
        await blobClient.DeleteIfExistsAsync();

        // Eliminar metadata de la base de datos
        _context.Files.Remove(fileMetadata);
        await _context.SaveChangesAsync();

        return Ok("File deleted successfully.");
    }


}
