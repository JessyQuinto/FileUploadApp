# FileUploadApp
# Aplicación de Carga de Archivos

Una aplicación web en .NET 8.0 que demuestra la funcionalidad de carga de archivos utilizando Azure Blob Storage y Azure SQL Database. Esta aplicación proporciona una solución simple pero robusta para la gestión de archivos con una interfaz API RESTful.

## Características

- Carga de archivos a Azure Blob Storage
- Almacenamiento de metadatos de archivos en Azure SQL Database
- Interfaz Swagger para documentación y prueba de API
- Capacidad de eliminación de archivos
- Integración con Razor Pages para interfaz web

## Requisitos Previos

- SDK de .NET 8.0
- Suscripción de Azure
- Cuenta de Azure Storage
- Base de datos Azure SQL

## Stack Tecnológico

- **Framework Backend**: ASP.NET Core 8.0
- **Base de Datos**: Azure SQL Database
- **Almacenamiento**: Azure Blob Storage
- **Documentación API**: Swagger/OpenAPI
- **ORM**: Entity Framework Core 8.0

## Paquetes NuGet

- Azure.Storage.Blobs (12.22.2)
- Microsoft.Data.SqlClient (5.2.2)
- Microsoft.EntityFrameworkCore (8.0.10)
- Microsoft.EntityFrameworkCore.SqlServer (8.0.10)
- Swashbuckle.AspNetCore (6.6.2)

## Configuración

La aplicación requiere la siguiente configuración en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "AzureSqlDatabase": "Tu_Cadena_De_Conexión_SQL_Azure"
  },
  "BlobStorage": {
    "ConnectionString": "Tu_Cadena_De_Conexión_Blob_Storage"
  }
}
```

## Esquema de Base de Datos

La aplicación utiliza un esquema simple de base de datos con una tabla `Files`:

- Id (int, clave primaria)
- FileName (string)
- FileType (string)
- UploadDate (DateTime)
- BlobUrl (string)

## Endpoints de la API

### Cargar Archivo
```
POST /api/FileUpload/upload
```
Carga un archivo a Azure Blob Storage y almacena sus metadatos en la base de datos.

### Eliminar Archivo
```
DELETE /api/FileUpload/delete/{id}
```
Elimina un archivo tanto de Azure Blob Storage como de la base de datos.

## Primeros Pasos

1. Clonar el repositorio
```bash
git clone [url-repositorio]
```

2. Actualizar las cadenas de conexión en `appsettings.json` con tus credenciales de Azure

3. Ejecutar las migraciones de la base de datos
```bash
dotnet ef database update
```

4. Ejecutar la aplicación
```bash
dotnet run
```

5. Acceder a la interfaz Swagger en `https://localhost:[puerto]/swagger`

## Configuración de Desarrollo

1. Asegúrate de tener instalado el SDK de .NET 8.0
2. Instalar los paquetes NuGet requeridos:
```bash
dotnet restore
```
3. Crear la base de datos y aplicar las migraciones:
```bash
dotnet ef database update
```

## Cómo Contribuir

1. Haz un fork del repositorio
2. Crea tu rama de característica (`git checkout -b feature/CaracteristicaIncreible`)
3. Haz commit de tus cambios (`git commit -m 'Agregada alguna CaracteristicaIncreible'`)
4. Haz push a la rama (`git push origin feature/CaracteristicaIncreible`)
5. Abre un Pull Request

## Notas de Seguridad

- Las cadenas de conexión e información sensible deben almacenarse en Azure Key Vault en producción
- Configurar las políticas CORS según sea necesario para tu entorno
- Implementar autenticación y autorización según los requisitos de tu caso de uso

## Licencia

Este proyecto está licenciado bajo la Licencia MIT - ver el archivo LICENSE para más detalles.
