using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teeleh.Models.Dtos
{
    public class Filter
    {
        public List<string> PlatformIds { get; set; }
        public float? MaxPrice { get; set; }
        public float? MinPrice { get; set; }
        public int? LocationCityId { get; set; }
        public int? LocationProvinceId { get; set; }
        public Advertisement.MediaType? MedType { get; set; }
        public int? PageNumber { get; set; }
        public Sort? Sort { get; set; }
    }

    public enum Sort
    {
        PRICE_ASCENDING,
        PRICE_DESCENDING,
        NEWEST
    }
}
