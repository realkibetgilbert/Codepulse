namespace Codepulse.API.Application.DTOs.ImageUpload
{
    public class BlogImageToDisplay
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
