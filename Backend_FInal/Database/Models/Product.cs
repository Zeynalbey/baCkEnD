using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class Product : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ProductCategory>? ProductCategories { get; set; }
        public List<BasketProduct>? BasketProducts { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
    }
}
