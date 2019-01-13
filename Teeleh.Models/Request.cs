using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Teeleh.Models.Enums;


namespace Teeleh.Models
{
    public class Request
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Game Game { get; set; }

        public int GameId { get; set; }

        public List<Platform> Platforms { get; set; }

        public FilterType FilterType { get; set; }

        public List<Location> LocationProvinces { get; set; }
        public List<Location> LocationCities { get; set; }
        public List<Location> LocationRegions { get; set; }
        
        
        public RequestMode ReqMode { get; set; }

        public bool IsDeleted { get; set; }

        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
    
}
