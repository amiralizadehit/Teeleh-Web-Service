using System.ComponentModel.DataAnnotations;

namespace Teeleh.Models.ViewModels
{
    public class UserSignUpEmailViewModel
    {
            [StringLength(11)]
            [Required]
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
            [Required]
            public string Email { get; set; }

            public string UniqueCode { get; set; }
        }
}
