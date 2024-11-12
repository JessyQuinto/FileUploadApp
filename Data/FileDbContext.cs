using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FileUploadApp.Data
{
    public class FileDbContext : DbContext
    {
        public FileDbContext(DbContextOptions<FileDbContext> options) : base(options) { }

        public DbSet<FileMetadata> Files { get; set; }
    }
}