using Backend_Final.Database.Models;
using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class ProductCategory:BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
