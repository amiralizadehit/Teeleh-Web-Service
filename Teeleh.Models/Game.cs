using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Teeleh.Models.Enums;

namespace Teeleh.Models
{


    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual Image Avatar { get; set; }

        public virtual Image Cover { get; set; }

        public virtual List<Image> GameplayImages { get; set; }

        public virtual List<Platform> SupportedPlatforms { get; set; }

        public List<Advertisement> Advertisements { get; set; }

        public List<PSNAccount> PSNAccounts { get; set; }

        public List<Request> Requests { get; set; }

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

        public bool isDeleted { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }

    

}
