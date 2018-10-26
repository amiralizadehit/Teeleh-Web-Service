using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Teeleh.Models.ViewModels
{
    public class GameFormViewModel
    {
        [Required]
        [StringLength(20,ErrorMessage = "You can not enter a name with more than 20 characters")]
        public string Name { get; set; }

       // [DisplayName("Upload avatar photo")]
        [Required]
        public HttpPostedFileBase ImageFile { get; set; }


        [Required]
        [DisplayName("Platform")]
        public IEnumerable<string> SelectedPlatforms { get; set; }

        public MultiSelectList Platforms { get; set; }
        

        [DisplayName("Release Date")]
        public string ReleaseDate { get; set; }

        public DateTime ParsedReleaseDate
        {
            get { return DateTime.Parse(ReleaseDate); }
        }

        [Required]
        [DisplayName("Meta Score")]
        [Range(0,100,ErrorMessage = "Meta Score can only be between 0 and 100")]
        public int MetaScore { get; set; }

        [DisplayName("User Score")]
        [Range(0,10,ErrorMessage = "User Score can only be between 0 and 10")]
        public float UserScore { get; set; }

        [Required]
        [DisplayName("Genre")]
        public IEnumerable<int> SelectedGenres { get; set; }

        public MultiSelectList Genres { get; set; }

        [StringLength(15, ErrorMessage="You can not enter a developer with more than 15 characters")]
        public string Developer { get; set; }

        [StringLength(15, ErrorMessage = "You can not enter a publisher with more than 15 characters")]
        public string Publisher { get; set; }

        [DisplayName("Online Capability")]
        public bool OnlineCapability { get; set; }

    }
}
