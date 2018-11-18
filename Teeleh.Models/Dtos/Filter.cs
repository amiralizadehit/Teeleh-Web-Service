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
        public float price { get; set; }
        public int LocationRegionId { get; set; }
        public int LocationCityId { get; set; }
        public int LocationProvinceId { get; set; }

    }
}
