using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class Blog :BaseEntity<int>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        public List<BlogDisplay>? BlogDisplays { get; set; }

        public List<BlogAndBlogCategory>? BlogAndCategories { get; set; }
        public List<BlogAndBlogTag>? BlogAndTags { get; set; }
    }
}
