namespace Codepulse.API.Domain.Entities
{
    public class Category:Base
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
