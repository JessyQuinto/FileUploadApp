using System;

namespace FileUploadApp.Data
{
    public class FileMetadata
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime UploadDate { get; set; }
        public string BlobUrl { get; set; }
    }
}
