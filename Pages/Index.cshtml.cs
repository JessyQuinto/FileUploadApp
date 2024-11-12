using FileUploadApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUploadApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FileDbContext _context;

        public IndexModel(FileDbContext context)
        {
            _context = context;
        }

        // Lista de archivos cargados para mostrar en la página
        public List<FileMetadata> Files { get; set; }

        // Cargar la lista de archivos desde la base de datoss
        public async Task<IActionResult> OnGetAsync()
        {
            Files = await _context.Files.ToListAsync();
            return Page();
        }
    }
}
