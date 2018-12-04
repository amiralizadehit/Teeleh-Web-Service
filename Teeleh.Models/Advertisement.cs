using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Teeleh.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public GameRegion GameReg { get; set; }
        public virtual User User { get; set; }
        public virtual Game Game { get; set; }
        public virtual Platform Platform { get; set; }


        public virtual Location LocationRegion { get; set; }

        public virtual Location LocationCity { get; set; }
    
        public virtual Location LocationProvince { get; set; }

        public virtual Image UserImage { get; set; }

        public int? LocationRegionId { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public int LocationCityId { get; set; }

        [Required]
        public int LocationProvinceId { get; set; }

        [Required]
        public string PlatformId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Caption { get; set; }

        [Required]
        public MediaType MedType { get; set; }

        public virtual List<Exchange> ExchangeGames { get; set; }
        public bool isDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }



        public enum MediaType
        {
            NEW,
            SECOND_HAND,
            DIGITAL_ACCOUNT
        }

        public enum GameRegion
        {
            ALL,
            R1,
            R2,
            R3,
            R4
        }
        
    }
}
