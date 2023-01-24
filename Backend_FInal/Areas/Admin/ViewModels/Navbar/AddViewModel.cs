using System.ComponentModel.DataAnnotations;

namespace Backend_Final.ViewModels.Admin.Navbar
{
    public class AddViewModel
    {

        [Required]
        public string Title { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }
   
    }
}
