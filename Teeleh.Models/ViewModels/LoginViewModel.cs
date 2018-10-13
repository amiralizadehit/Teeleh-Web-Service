using System.ComponentModel.DataAnnotations;

namespace Teeleh.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string Password { get; set; }

        public string UniqueCode { get; set; }

    }
}
