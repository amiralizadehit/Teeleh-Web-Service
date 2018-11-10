using System;
using System.Collections.Generic;

namespace Teeleh.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public virtual User User { get; set; }
        public virtual Game Game { get; set; }
        public virtual Platform Platform { get; set; }
        public virtual Location Location { get; set; }
        public virtual Image UserImage { get; set; }
        public int GameId { get; set; }
        public int LocationId { get; set; }
        public string PlatformId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Caption { get; set; }
        public AdvertisementType AdType { get; set; }
        public virtual List<Exchange> ExchangeGames { get; set; }
        public bool isDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }



        public enum AdvertisementType
        {
            NEW,
            SECOND_HAND,
            DIGITAL_ACCOUNT
        }
        
    }
}
