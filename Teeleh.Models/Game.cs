using System;
using System.Collections.Generic;

namespace Teeleh.Models
{


    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Image Avatar { get; set; }
        public List<Platform> SupportedPlatforms { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float MetaScore { get; set; }
        public float UserScore { get; set; }
        public List<Genre> Genres { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public bool OnlineCapability { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
