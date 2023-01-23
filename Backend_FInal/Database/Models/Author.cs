using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class Author : BaseEntity<int>, IAuditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Product> Products { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
