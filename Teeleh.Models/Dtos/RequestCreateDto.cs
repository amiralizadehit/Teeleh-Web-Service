
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Teeleh.Models.CustomValidation;
using Teeleh.Models.Enums;


namespace Teeleh.Models.Dtos
{
    public class RequestCreateDto :PairDto
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public List<string> SelectedPlatforms { get; set; }

        [Required]
        [ValidEnumValue]
        public FilterType FilterType { get; set; }

        public int? LocationProvince { get; set; }
        public int? LocationCity { get; set; }
        public int? LocationRegion { get; set; }

        [Required]
        [ValidEnumValue]
        public RequestMode ReqMode { get; set; }

        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }

    }
}
