using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Areas.Admin.ViewModels.Navbar
{
    public class AddViewModel
    {

        [Required]
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsBold { get; set; }
        public int Order { get; set; }
    }
}
