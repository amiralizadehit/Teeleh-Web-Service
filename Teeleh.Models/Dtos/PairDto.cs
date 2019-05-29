using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.ViewModels;

namespace Teeleh.Models.Dtos
{
    public class PairDto
    {
        [Required] public SessionInfoObject Session;
    }
    public class TokenPairDto : PairDto
    {
        [Required] public string Token { get; set; }
    }

    public class NoncePairDto : PairDto
    {
        [Required]
        [Range(10000, 99999)]
        public int Nonce { get; set; }
    }

    public class PhoneNumberPairDto : PairDto
    {
        [Required] [Phone] public string PhoneNumber { get; set; }
    }

    public class IDPairDto : PairDto
    {
        [Required] public int Id { get; set; }
    }

    public class EmailPairDto : PairDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class PasswordPairDto : PairDto
    {
        [Required]
        public string Password;
    }

    public class UserInfoDto : PairDto
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityId { get; set; }
        public string PSNId { get; set; }
        public string XBOXLive { get; set; }
    }
}




