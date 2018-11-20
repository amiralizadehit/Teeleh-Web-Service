using System.Collections.Generic;

namespace Teeleh.Models
{
    public class Location
    {
        public int Id { get; set; }
        public Location Parent { get; set; }
        public int? ParentId { get; set; }
        public LocationType Type { get; set; }
        public string  Name { get; set; }

        public List<Advertisement> Advertisements { get; set; }

        public enum LocationType
        {
            PROVINCE,
            CITY,
            REGION
        }
   }
}
