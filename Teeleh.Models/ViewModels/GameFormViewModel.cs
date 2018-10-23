using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Teeleh.Models.ViewModels
{
    public class GameFormViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Avatar Avatar { get; set; }

        [Required]
        [DisplayName("Platform")]
        public IEnumerable<string> SelectedPlatforms { get; set; }

        public MultiSelectList Platforms { get; set; }
        

        [DisplayName("Release Date")]
        public string ReleaseDate { get; set; }

        [Required]
        [DisplayName("Meta Score")]
        public string MetaScore { get; set; }

        [DisplayName("User Score")]
        public string UserScore { get; set; }

        [Required]
        [DisplayName("Genre")]
        public IEnumerable<int> SelectedGenres { get; set; }

        public MultiSelectList Genres { get; set; }

        public string Developer { get; set; }
        public string Publisher { get; set; }

        [DisplayName("Online Capability")]
        public bool OnlineCapability { get; set; }

    }
}
