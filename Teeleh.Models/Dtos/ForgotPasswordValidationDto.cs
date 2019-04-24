using System.ComponentModel.DataAnnotations;

namespace Teeleh.Models.Dtos
{
    public class ForgotPasswordValidationDto
    {
        [Required]
        [Range(10000, 99999)]
        public int ForgetPassCode { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
