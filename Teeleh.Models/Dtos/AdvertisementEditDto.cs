using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.Migrations;
using Teeleh.Models.ViewModels;

namespace Teeleh.Models.Dtos
{
    public class AdvertisementEditDto : PairDto
    {
      
        [Required] public int Id { get; set; }

        public float Price { get; set; }

        [Required]
        public int LocationCityId { get; set; }

        [Required]
        public int LocationProvinceId { get; set; }

        public int? LocationRegionId { get; set; }

        public string UserImage { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string Caption { get; set; }

        [Required]
        public int MedType { get; set; }

        public List<int> ExchangeGames { get; set; }
    }
}