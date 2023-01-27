using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Areas.Admin.ViewModels.Payment
{
    public class AddViewModel
    {
        public int Id { get; set; } 
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
        public IFormFile? Iconİmage { get; set; }
        public string? IconImageUrl { get; set; }
    }
}
