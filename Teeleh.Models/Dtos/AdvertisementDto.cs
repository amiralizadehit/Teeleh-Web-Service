using System.Collections.Generic;
using Teeleh.Models.ViewModels;


namespace Teeleh.Models.Dtos
{
    public class AdvertisementDto
    {
        public SessionInfoObject SessionInfo { get; set; }
        public float Price { get; set; }
        public int GameId { get; set; }
        public string PlatformId { get; set; }
        public int LocationId { get; set; }
        public byte[] UserImage { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Caption { get; set; }
        public int AdType { get; set; }
        public List<int> ExchangeGames { get; set; }

    }
}