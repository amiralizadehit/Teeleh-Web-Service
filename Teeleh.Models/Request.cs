using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Management;
using Teeleh.Models.Enums;


namespace Teeleh.Models
{
    public class Request
    {
        public int Id { get; set; }

        [Required]
        public virtual User User { get; set; }

        
        public Game Game { get; set; }

        [Required]
        public int GameId { get; set; }

        public virtual List<Platform> Platforms { get; set; }

        [Required]
        public FilterType FilterType { get; set; }

        public Location LocationProvince { get; set; }
        
        public Location LocationCity { get; set; }
        
        public Location LocationRegion { get; set; }
        

        public int? LocationProvinceId { get; set; }
        public int? LocationCityId { get; set; }
        public int? LocationRegionId { get; set; }
        
        [Required]
        public RequestMode ReqMode { get; set; }

        public bool IsDeleted { get; set; }

        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
    
}
