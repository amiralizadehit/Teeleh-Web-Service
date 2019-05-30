using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.Enums;

namespace Teeleh.Models.Panel
{
    public class AdminSession
    {
        public int Id { get; set; }

        public string SessionKey { get; set; }

        public DateTime InitMoment { get; set; }
        public DateTime? DeactivationMoment { get; set; }

        public SessionState State { get; set; }

        public Admin Admin { get; set; }
        public int AdminId { get; set; }
    }
}
