using Backend_Final.Database.Models.Common;
using Backend_Final.Database.Models.Enums;

namespace Backend_Final.Database.Models
{
    public class Payment : BaseEntity<int>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? IconImage { get; set; }
        public string? IconİmageInSystem { get; set; }
    }
}
