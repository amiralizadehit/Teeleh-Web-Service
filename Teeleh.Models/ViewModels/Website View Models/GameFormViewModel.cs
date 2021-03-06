﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Web;
using System.Web.Mvc;
using Teeleh.Models.Enums;

namespace Teeleh.Models.ViewModels
{
    public class GameFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50,ErrorMessage = "You can not enter a name with more than 20 characters")]
        public string Name { get; set; }

       // [DisplayName("Upload avatar photo")]
        public HttpPostedFileBase AvatarImage { get; set; }

        public HttpPostedFileBase CoverImage { get; set; }

        public HttpPostedFileBase[] GameplayImages { get; set; }

        public GameplayImageOption gameplayOption { get; set; }

        //These are used for edit page
        public string AvatarImagePath { get; set; }

        public string CoverImagePath { get; set; }

        public string[] GameplayImagesPath { get; set; }


        [Required(ErrorMessage = "You should select at least one platform for this game")]
        [DisplayName("Platform")]
        public List<string> SelectedPlatforms { get; set; }

        public MultiSelectList Platforms { get; set; }
        

        [Required]
        [DisplayName("Meta Score")]
        [Range(0,100,ErrorMessage = "Meta Score can only be between 0 and 100")]
   
        public int MetaScore { get; set; }

        [Required]
        [DisplayName("User Score")]
        [Range(0,10,ErrorMessage = "User Score can only be between 0 and 10")]
        
        public float UserScore { get; set; }

        [Required(ErrorMessage = "You should select at least one genre for this game")]
        [DisplayName("Genre")]
        public List<int> SelectedGenres { get; set; }

        public MultiSelectList Genres { get; set; }

        [Required]
        [StringLength(25, ErrorMessage="You can not enter a developer with more than 25 characters")]
        public string Developer { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "You can not enter a publisher with more than 25 characters")]
        public string Publisher { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Select a valid ESRB rating")]
        [DisplayName("ESRB Rating")]
        public ESRB ESRBRating { get; set; }

        [Required]
        [DisplayName("Online Capability")]
        public bool OnlineCapability { get; set; }

        [DisplayName("Release Date")]
        [Required]
        [ValidDate]
        public string ReleaseDate { get; set; }

        public DateTime GetReleaseDate()
        {
            return DateTime.Parse(ReleaseDate).Date;
        }

    }
}
