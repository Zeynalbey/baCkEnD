using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Areas.Admin.ViewModels.Slider
{
    public class AddViewModel
    {
        [Required]
        public string? MainTitle { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public string? SecondTitle { get; set; }
        [Required]
        public string? Button { get; set; }
        [Required]
        public string? ButtonRedirectUrl { get; set; }
        public int Order { get; set; }
        public IFormFile? İmage { get; set; }
    }
}
