using Backend_Final.Database.Models.Common;
using Backend_Final.Database.Models.Enums;

namespace Backend_Final.Database.Models
{
    public class OrderProduct : BaseEntity<int>, IAuditable
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string? OrderId { get; set; }
        public Order? Order { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
