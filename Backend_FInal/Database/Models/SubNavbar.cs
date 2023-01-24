using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class SubNavbar : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Url { get; set; } 
        public int Order { get; set; }
        public int NavbarId { get; set; }
        public Navbar Navbar { get; set; }
    }
}
