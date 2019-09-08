using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.Enums;

namespace Teeleh.Models
{
    public class PSNAccount
    {
        public int Id { get; set; }
        public List<Game> Games { get; set; }
        public User User { get; set; }
        public float Price { get; set; }
        public PSNAccountCapacity Capacity { get; set; } 
        public PSNAccountType Type { get; set; }
        public bool HasPlus { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
