using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.Enums;

namespace Teeleh.Models
{
    public class PSNAccountRequest
    {
        public int Id { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public List<Game> Games { get; set; }

        [Required]
        public PSNAccountType Type { get; set; }

        public PSNAccountRegion? Region { get; set; }

        public RequestMode? Capacity { get; set; }

        [Required]
        public float MinPrice { get; set; }

        [Required]
        public float MaxPrice { get; set; }

        public bool? HasPlus { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
 
        public DateTime UpdatedAt { get; set; }
    }
}
