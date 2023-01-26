

using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class Color : BaseEntity<int>
    {
        public string? Name { get; set; }
        public List<ProductColor>? ProductColors { get; set; }    
    }
}
