using System;
using System.Collections.Generic;

namespace Teeleh.Models
{


    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Avatar Avatar { get; set; }
        public IEnumerable<Platform> SupportedPlatforms { get; set; }
        public string PlatformId { get; set; }
        public DateTime ReleaseDate { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
