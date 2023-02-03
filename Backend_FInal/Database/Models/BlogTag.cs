using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class BlogTag : BaseEntity<int>
    {
        public string? Title { get; set; }
        public List<BlogAndBlogTag>? Tags { get; set; }
    }
}
