using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.Enums;


namespace Teeleh.Models
{
    public class Notification
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int? AdvertisementId { get; set; }
        
        public virtual Advertisement Advertisement { get; set; }

        public virtual Image Avatar { get; set; }

        public int AvatarId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public NotificationStatus Status { get; set; }
        [Required]
        public NotificationType Type { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
