using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Teeleh.Models.Enums;

namespace Teeleh.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Range(10000, 99999)] public int? Nonce { get; set; }

        public DateTime InitMoment { get; set; }
        public DateTime? ActivationMoment { get; set; }
        public DateTime? DeactivationMoment { get; set; }

        [Required] [StringLength(32)] public string SessionKey { get; set; }

        public string UniqueCode { get; set; }
        public State State { get; set; }

        public string FCMToken { get; set; }

        public SessionPlatform SessionPlatform { get; set; }

        [ForeignKey("User")] public int? User_Id { get; set; }

        public virtual User User { get; set; }
    }
}