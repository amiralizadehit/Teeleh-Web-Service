using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teeleh.Models
{
    public enum SessionState { Pending, Active, Deactivate, Abolished }
    public class Session
    {
        public int Id { get; set; }

        [Range(10000,99999)]
        public int? Nonce { get; set; }

        public DateTime InitMoment { get; set; }
        public DateTime? ActivationMoment { get; set; }
        public DateTime? DeactivationMoment { get; set; }

        [Required]
        [StringLength(32)]
        public string SessionKey { get; set; }

        public string UniqueCode { get; set; }
        public SessionState State { get; set; }

        [ForeignKey("User")]
        public int? User_Id { get; set; }

        public virtual User User { get; set; }
    }
}
