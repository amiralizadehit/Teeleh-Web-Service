using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teeleh.Models
{
    /// <summary>
    /// The DB model for many 2 many relationship between user and advertisement.
    /// </summary>
    public class AdBookmark
    {
        public User User { get; set; }

        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }
        public virtual Advertisement Advertisement { get; set; }

        [Key]
        [Column(Order = 2)]
        public int AdvertisementId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
