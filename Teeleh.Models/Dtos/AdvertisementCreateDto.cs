using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Teeleh.Models.CustomValidation;
using Teeleh.Models.Enums;
using Teeleh.Models.ViewModels;


namespace Teeleh.Models.Dtos
{
    public class AdvertisementCreateDto :PairDto
    {
       
        public float Price { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public string PlatformId { get; set; }


        public int? LocationRegionId { get; set; }

        [Required]
        public int LocationCityId { get; set; }

        [Required]
        public int LocationProvinceId { get; set; }

        [Required]
        [ValidEnumValue]
        public GameRegion GameReg { get; set; }

        public string UserImage { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string Caption { get; set; }

        [Required]
        [ValidEnumValue]
        public MediaType MedType { get; set; }

        public List<int> ExchangeGames { get; set; }

    }
}