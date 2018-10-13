using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Teeleh.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string PSNId { get; set; }

        public string XBOXLive { get; set; }

        public Avatar UserAvatar { get; set; }

        public int ForgetPassCode { get; set; }

        public SessionState State { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }


        public virtual IEnumerable<Session> Sessions { get; set; }
    }
}
