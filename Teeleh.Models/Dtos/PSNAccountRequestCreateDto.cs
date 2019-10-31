using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.CustomValidation;
using Teeleh.Models.Enums;

namespace Teeleh.Models.Dtos
{
    public class PSNAccountRequestCreateDto : PairDto
    {
        public int Id { get; set; }

        [Required]
        public List<int> Games { get; set; }

        [Required]
        [ValidEnumValue]
        public PSNAccountType Type { get; set; }

        [ValidEnumValue]
        public PSNAccountRegion? Region { get; set; }

        [ValidEnumValue]
        public RequestMode? Capacity { get; set; }

        [Required]
        public float MinPrice { get; set; }

        [Required]
        public float MaxPrice { get; set; }

        public bool? HasPlus { get; set; }
    }
}
