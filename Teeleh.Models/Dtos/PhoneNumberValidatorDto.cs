using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teeleh.Models.Dtos
{
    public class PhoneNumberValidatorDto :PairDto
    {
        [Phone]
        [StringLength(11)]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
