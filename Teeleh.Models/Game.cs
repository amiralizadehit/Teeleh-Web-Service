using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Teeleh.Models
{


    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual Image Avatar { get; set; }

        public virtual List<Platform> SupportedPlatforms { get; set; }

        public List<Advertisement> Advertisements { get; set; }

        public List<Exchange> ToExchangeWith { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
        public int MetaScore { get; set; }
        public float UserScore { get; set; }

        public virtual List<Genre> Genres { get; set; }

        public string Developer { get; set; }

        public string Publisher { get; set; }

        public bool OnlineCapability { get; set; }

        public ESRB Rating { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }

    public enum ESRB
    {
       [Display(Name = "E")]
        E = 1,
       [Display(Name = "E +10")]
        E10 = 2,
       [Display(Name = "T")]
        T = 3,
       [Display(Name = "M")]
        M = 4,
       [Display(Name = "AO")]
        AO = 5
    }

}
