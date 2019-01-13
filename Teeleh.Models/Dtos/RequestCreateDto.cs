using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.ViewModels;

namespace Teeleh.Models.Dtos
{
    public class RequestCreateDto
    {

        [Required]
        public SessionInfoObject SeesionInfoObject { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public List<string> SelectedPlatforms { get; set; }

        [Required]
        public int FilterType { get; set; }

        public List<int> SelectedProvinces { get; set; }
        public List<int> SelectedCities { get; set; }
        public List<int> SelectedRegions { get; set; }

        [Required]
        public int ReqMode { get; set; }

        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }

    }
}
