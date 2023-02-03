using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class BlogAndBlogCategory : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategory? Category { get; set; }
    }
}
