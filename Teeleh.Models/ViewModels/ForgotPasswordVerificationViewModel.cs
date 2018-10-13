using System.ComponentModel.DataAnnotations;

namespace Teeleh.Models.ViewModels
{
    public class ForgotPasswordVerificationViewModel
    {
        [Required]
        [Range(10000, 99999)]
        public int ForgetPassCode { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
