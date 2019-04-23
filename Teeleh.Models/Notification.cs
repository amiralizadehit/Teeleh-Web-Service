using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.Enums;
using Teeleh.Utilities.Enum;

namespace Teeleh.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int AdvertisementId { get; set; }
        public virtual Advertisement Advertisement { get; set; }
        public string Message { get; set; }
        public NotificationStatus Status { get; set; }
        public NotificationType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
