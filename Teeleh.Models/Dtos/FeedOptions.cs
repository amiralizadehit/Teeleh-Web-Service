using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.Enums;

namespace Teeleh.Models.Dtos
{
    public class FeedOptions
    {
        public Filter Filter { get; set; }
        public Sort Sort { get; set; }
        public string Search { get; set; }
    }

    public class Filter
    {
        public List<string> PlatformIds { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinPrice { get; set; }
        public int? LocationCityId { get; set; }
        public int? LocationProvinceId { get; set; }
        public Enums.MediaType? MedType { get; set; }
        public int? PageNumber { get; set; }
        public Sort? Sort { get; set; }
    }

    
    
}
