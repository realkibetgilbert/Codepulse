using Codepulse.API.Application.DTOs.Category;

namespace Codepulse.API.Application.DTOs.BlogPost
{
    public class BlogPostToDisplay
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }
        public List<CategoryToDisplayDto> Categories { get; set; }= new List<CategoryToDisplayDto>();
    }
}
