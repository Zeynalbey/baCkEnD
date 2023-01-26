using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class Navbar : BaseEntity<int>
    {
   
        public string? Title { get; set; }
        public string? Url { get; set; }
        public bool IsBold  { get; set; }
        public int Order { get; set; }
        public List<SubNavbar>? Subnavbars { get; set; }

    }
}
