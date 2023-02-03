using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Areas.Client.ViewModels.Authentication
{
    public class AccountInfoViewModel
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? Lastname { get; set; }
    }
}
