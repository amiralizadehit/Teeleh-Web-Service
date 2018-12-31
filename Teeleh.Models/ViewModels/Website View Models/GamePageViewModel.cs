using System;
using System.Collections.Generic;

namespace Teeleh.Models.ViewModels.Website_View_Models
{
    public class GamePageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public List<string> SupportedPlatforms { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int MetaScore { get; set; }
        public float UserScore { get; set; }
        public List<string> Genres { get; set; }
        public string Developer { get; set; }
        public string Rating { get; set; }

    }
}
