using System.ComponentModel.DataAnnotations;
using Teeleh.Models.CustomValidation;

namespace Teeleh.Models.Dtos
{
    public class LoginDto
    {
        [StringLength(11)]
        [Phone]
        [PhoneOrEmail]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        public string UniqueCode { get; set; }

        public string FCMToken { get; set; }

        public int SessionPlatform { get; set; }

    }
}
