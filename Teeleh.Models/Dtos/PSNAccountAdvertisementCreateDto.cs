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
    public class PSNAccountAdvertisementCreateDto : PairDto
    {

        [Required]
        public List<int> Games { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        [ValidEnumValue]
        public PSNAccountType Type { get; set; }

        [Required]
        [ValidEnumValue]
        public PSNAccountRegion Region { get; set; }

        [ValidEnumValue]
        public PSNAccountCapacity? Capacity { get; set; }

        public bool? HasPlus { get; set; }

        public string UserImage { get; set; }

        public string Caption { get; set; }
    }
}
