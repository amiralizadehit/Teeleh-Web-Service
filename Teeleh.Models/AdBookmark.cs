using System;
using System.Collections.Generic;
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
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public virtual Advertisement Advertisement { get; set; }
        public int AdvertisementId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
