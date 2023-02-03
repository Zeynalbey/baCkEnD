using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class BlogCategory : BaseEntity<int>
    {
        public string? Title { get; set; }
        public List<BlogCategory>? Categories { get; set; }
        public List<BlogAndBlogCategory>? BlogAndCategories { get; set; }

    }
}
