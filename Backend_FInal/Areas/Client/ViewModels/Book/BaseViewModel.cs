using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Areas.Client.ViewModels.Product
{
    public abstract class BaseViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public decimal? Price { get; set; }
    }
}
