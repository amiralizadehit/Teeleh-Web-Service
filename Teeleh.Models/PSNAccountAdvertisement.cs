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
    public class PSNAccountAdvertisement
    {
        public int Id { get; set; }

        [Required]
        public List<Game> Games { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public float Price { get; set; }

        public PSNAccountCapacity? Capacity { get; set; }
       
        [Required]
        public PSNAccountType Type { get; set; }

        public virtual Image UserImage { get; set; }

        public string Caption { get; set; }

        [Required]
        public PSNAccountRegion Region { get; set; }
        
        public bool? HasPlus { get; set; }

        public List<Notification> Notifications { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
