using Backend_Final.Contracts.ProductImage;
using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class ProductImage : BaseEntity<int>, IAuditable
    {
        public string? ImageName { get; set; }
        public string? ImageNameInFileSystem { get; set; } 

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
