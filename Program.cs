using Azure.Storage.Blobs;
using FileUploadApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agrega Razor Pages al servicio
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Configura el DbContext para la base de datos usando la cadena de conexión de appsettings.json
builder.Services.AddDbContext<FileDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlDatabase")));

// Configura el BlobServiceClient usando la cadena de conexión desde appsettings.json
var blobConnectionString = builder.Configuration.GetSection("BlobStorage:ConnectionString").Value;
builder.Services.AddSingleton(x => new BlobServiceClient(blobConnectionString));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

// Mapea las Razor Pages y los controladores
app.MapRazorPages();
app.MapControllers();

app.Run();
