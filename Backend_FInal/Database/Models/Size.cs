using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class Size : BaseEntity<int>,IAuditable
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
    }
}
