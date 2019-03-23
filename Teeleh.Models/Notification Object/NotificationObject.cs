using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.Enums;

namespace Teeleh.Models.Notification_Object
{
    public class NotificationObject
    {
        public int Id;
        public string Avatar;
        public string Message;
        public NotificationType type;
    }
}
