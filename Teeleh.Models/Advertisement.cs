using System;
using System.Collections.Generic;

namespace Teeleh.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public User User { get; set; }
        public string PlatformId { get; set; }
        public Location Location { get; set; }
        public IEnumerable<Game> GamesToExchange { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
    }
}
