using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class Product : BaseEntity<int>, IAuditable
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ProductColor>? ProductColors { get; set; }
        public List<ProductTag>? ProductTags { get; set; }
        public List<ProductCategory>? ProductCategories { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }


    }
}
