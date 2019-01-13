using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Teeleh.Models.Enums;


namespace Teeleh.Models
{
    public class Request
    {
        public int Id { get; set; }
        public Game Game { get; set; }

        public List<Platform> Platform { get; set; }

        public FilterType FilterType { get; set; }

        public List<Location> LocationProvince { get; set; }
        public List<Location> LocationCity { get; set; }
        public List<Location> LocationRegion { get; set; }
        
        
        public RequestMode ReqMode { get; set; }

        public bool IsDeleted { get; set; }

        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
    
}
