using System.ComponentModel.DataAnnotations;
using Teeleh.Models.CustomValidation;


namespace Teeleh.Models.ViewModels
{
    public class UserSignUpViewModel
    {
        [StringLength(11)]
        [Phone]
        [PhoneOrEmail]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string UniqueCode { get; set; }
    }
}