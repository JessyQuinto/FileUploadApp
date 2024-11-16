using Azure.Storage.Blobs;
using FileUploadApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<FileDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlDatabase")));

var blobConnectionString = builder.Configuration.GetSection("BlobStorage:ConnectionString").Value;
builder.Services.AddSingleton(x => new BlobServiceClient(blobConnectionString));

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

// Redirige la raíz "/" a tu página Razor predeterminada
app.MapGet("/", context =>
{
    context.Response.Redirect("/"); // Redirigir al Razor Page "Index.cshtml"
    return Task.CompletedTask;
});

// Mapear Razor Pages y controladores
app.MapRazorPages(); // Necesario para Razor Pages
app.MapControllers(); // Necesario para las APIs

app.Run();
