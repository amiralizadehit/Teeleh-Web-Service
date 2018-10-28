using System;
using System.Collections.Generic;


namespace Teeleh.Models.Dtos
{
    public class AdvertisementDto
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public int UserId { get; set; }
        public Game Game { get; set; }
        public string PlatformId { get; set; }
        public Location Location { get; set; }
        public Image UserImage { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string caption { get; set; }
        public AdvertisementType AdType { get; set; }
        public List<Game> GamesToExchange { get; set; }
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