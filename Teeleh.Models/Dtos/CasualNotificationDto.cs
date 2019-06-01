using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Teeleh.Models.Dtos
{
    public class CasualNotificationDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Message { get; set; }

        public HttpPostedFileBase Avatar { get; set; }
    }
}
