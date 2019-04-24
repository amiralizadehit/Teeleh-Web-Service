using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.ViewModels;

namespace Teeleh.Models.Dtos
{
    public class RequestCreateDto :PairDto
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public List<string> SelectedPlatforms { get; set; }

        [Required]
        public int FilterType { get; set; }

        public int? LocationProvince { get; set; }
        public int? LocationCity { get; set; }
        public int? LocationRegion { get; set; }

        [Required]
        public int ReqMode { get; set; }

        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }

    }
}
