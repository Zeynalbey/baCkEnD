using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class Slider : BaseEntity<int>, IAuditable
    {
        public string? MainTitle { get; set; }
        public string? Content { get; set; }
        public string? SecondTitle { get; set; }
        public string? Button { get; set; }
        public string? ButtonRedirectUrl { get; set; }
        public int Order { get; set; }
        public string? İmage { get; set; }
        public string? İmageInSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
