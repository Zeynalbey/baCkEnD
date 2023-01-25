using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Areas.Admin.ViewModels.Payment
{
    public class AddViewModel
    {
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
        public IFormFile? Iconİmage { get; set; }
    }
}
