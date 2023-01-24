using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Areas.Admin.ViewModels.ProductImage
{
    public class AddViewModel
    {
        public IFormFile? Image { get; set; }
    }
}
