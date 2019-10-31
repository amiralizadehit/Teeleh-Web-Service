using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teeleh.Models
{
    public class UserPhoneNumberValidator
    {
        public int Id { get; set; }

        public User User { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(11)]
        [Phone]
        public string TargetNumber { get; set; }
        [Required]
        public string SecurityToken { get; set; }
        [Required]
        public bool IsValidated { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? ValidatedAt { get; set; }
    }
}
