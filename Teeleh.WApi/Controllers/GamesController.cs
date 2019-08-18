using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Teeleh.Models;
using Teeleh.Models.CustomValidation.Website;
using Teeleh.Models.Enums;
using Teeleh.Models.Helper;
using Teeleh.Models.ViewModels;
using Teeleh.Models.ViewModels.Website_View_Models;
using Teeleh.Utilities;
using Teeleh.WApi.Functions;

namespace Teeleh.WApi.Controllers
{
    /// <summary>
    /// This class is used to manage games
    /// </summary>
    public class GamesController : Controller
    {
        private readonly AppDbContext db;


        public GamesController()
        {
            db = new AppDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        [SessionTimeout]
        public ActionResult Index()
        {
            //var games = db.Games.Include(p => p.SupportedPlatforms).Include(g => g.Genres).Include(a=>a.Avatar).ToList();
            var games = db.Games.Where(QueryHelper.GetGameValidationQuery()).ToList();

            var gamesViewModel = new List<GamePageViewModel>();
            foreach (var game in games)
            {
                var viewModel = new GamePageViewModel()
                {
                    Id = game.Id,
                    ImagePath = Url.Content(game.Avatar.ImagePath),
                    Genres = game.Genres.Select(t => t.Name).ToList(),
                    Developer = game.Developer,
                    Rating = (game.Rating > 0) ? game.Rating.ToString() : "Not Specified",
                    Name = game.Name,
                    MetaScore = game.MetaScore,
                    UserScore = game.UserScore,
                    SupportedPlatforms = game.SupportedPlatforms.Select(p => p.Name).ToList(),
                    ReleaseDate = game.ReleaseDate
                };
                gamesViewModel.Add(viewModel);
            }

            return View(gamesViewModel);
        }

        [SessionTimeout]
        public ActionResult Edit(int id)
        {
            var viewModel = new GameFormViewModel();

            var game = db.Games.SingleOrDefault(g => g.Id == id);

            if (game == null)
            {
                return HttpNotFound();
            }

            var genres = db.Genres.Select(g => new
            {
                GenreId = g.Id,
                GenreName = g.Name
            }).ToList();

            var platforms = db.Platforms.Select(p => new
            {
                PlatformId = p.Id,
                PlatformName = p.Name
            }).ToList();

            viewModel.Platforms = new MultiSelectList(platforms, "PlatformId", "PlatformName");
            List<string> supportedPlatformsIds = new List<string>();

            foreach (var gameSupportedPlatform in game.SupportedPlatforms)
            {
                supportedPlatformsIds.Add(gameSupportedPlatform.Id);
            }

            viewModel.SelectedPlatforms = supportedPlatformsIds;
            List<int> genreIds = new List<int>();
            foreach (var gameGenre in game.Genres)
            {
                genreIds.Add(gameGenre.Id);
            }

            var num = game.GameplayImages.Count;
            List<string> gameplayImages = new List<string>(num);
            foreach (var image in game.GameplayImages)
            {
                gameplayImages.Add(Url.Content(image.ImagePath));
            }
            


            viewModel.Genres = new MultiSelectList(genres, "GenreId", "GenreName");
            viewModel.SelectedGenres = genreIds;
            viewModel.Developer = game.Developer;
            viewModel.MetaScore = game.MetaScore;
            viewModel.Name = game.Name;
            viewModel.OnlineCapability = game.OnlineCapability;
            viewModel.ReleaseDate = game.ReleaseDate.ToString("d MMM yyyy");
            viewModel.Publisher = game.Publisher;
            viewModel.ESRBRating = game.Rating;
            viewModel.AvatarImagePath = Url.Content(game.Avatar.ImagePath);
            viewModel.CoverImagePath = (game.Cover == null) ? null : Url.Content(game.Cover.ImagePath);
            viewModel.GameplayImagesPath = gameplayImages.ToArray();
            viewModel.UserScore = game.UserScore;

            viewModel.Id = id;

            return View("Create", viewModel);
        }

        [SessionTimeout]
        public ActionResult Delete(int id)
        {
            var gameToDelete = db.Games.Include(f=>f.Avatar).Single(g => g.Id == id);
            gameToDelete.isDeleted = true;

            db.SaveChanges();

            return RedirectToAction("Index", "Games");
        }

        [SessionTimeout]
        public ActionResult Create()
        {
            var viewModel = new GameFormViewModel();

            var genres = db.Genres.Select(g => new
            {
                GenreId = g.Id,
                GenreName = g.Name
            }).ToList();

            var platforms = db.Platforms.Select(p => new
            {
                PlatformId = p.Id,
                PlatformName = p.Name
            }).ToList();

            viewModel.Id = -1;

            viewModel.Platforms = new MultiSelectList(platforms, "PlatformId", "PlatformName");
            //viewModel.SelectedPlatforms = new[] {Platform.PC,Platform.Switch,Platform.Android};
            viewModel.Genres = new MultiSelectList(genres, "GenreId", "GenreName");
            //viewModel.SelectedGenres = new[] {1, 3, 4};

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionTimeout]
        public ActionResult Create(GameFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var genres = db.Genres.Select(g => new
                {
                    GenreId = g.Id,
                    GenreName = g.Name
                }).ToList();
                var platforms = db.Platforms.Select(p => new
                {
                    PlatformId = p.Id,
                    PlatformName = p.Name
                }).ToList();

                viewModel.Platforms = new MultiSelectList(platforms, "PlatformId", "PlatformName");
                //viewModel.SelectedPlatforms = new[] { Platform.PC, Platform.Switch, Platform.Android };
                viewModel.Genres = new MultiSelectList(genres, "GenreId", "GenreName");
                //viewModel.SelectedGenres = new[] { 1, 3, 4 };
                return View("Create", viewModel);
            }

            var folderRandomIndex = RandomHelper.RandomInt(0, 10000);
            bool defaultCoverUsed = false;
            bool defaultAvatarUsed = false;

            Image avatarImage;
            if (viewModel.AvatarImage != null)
            {
                avatarImage =
                    ImageHandler.CreateWebImage(viewModel.AvatarImage, viewModel.Name, WebImageType.AVATAR);
            }
            else
            {
                //Default
                avatarImage = ImageHandler.CreateDefaultImage(viewModel.Name, WebImageType.AVATAR);
                defaultAvatarUsed = true;
            }

            Image coverImage;
            if (viewModel.CoverImage != null)
            {
                coverImage =
                    ImageHandler.CreateWebImage(viewModel.CoverImage, viewModel.Name, WebImageType.COVER);
            }
            else
            {
                //Here we set the default photo for cover image - coverPhotoFilePath= ...
                coverImage = ImageHandler.CreateDefaultImage(viewModel.Name, WebImageType.COVER);
                defaultCoverUsed = true;
            }

            var num = viewModel.GameplayImages.Length;
            List<Image> gameplayImages = new List<Image>(num);
            if (num > 0)
            {
                foreach (var viewModelGameplayImage in viewModel.GameplayImages)
                {
                    if (viewModelGameplayImage != null)
                    {
                        gameplayImages.Add(ImageHandler.CreateWebImage(viewModelGameplayImage,avatarImage.Name,WebImageType.GAMEPLAY));
                    }
                }
            }


            var selectedGenres = db.Genres.Where(g => viewModel.SelectedGenres.Contains(g.Id)).ToList();
            var selectedPlatforms = db.Platforms.Where(p => viewModel.SelectedPlatforms.Contains(p.Id)).ToList();

            if (viewModel.Id == -1) //New
            {
                
                var game = new Game
                {
                    Name = viewModel.Name,
                    Genres = selectedGenres,
                    Developer = viewModel.Developer,
                    Publisher = viewModel.Publisher,
                    MetaScore = viewModel.MetaScore,
                    UserScore = viewModel.UserScore,
                    OnlineCapability = viewModel.OnlineCapability,
                    Rating = viewModel.ESRBRating,
                    ReleaseDate = viewModel.GetReleaseDate(),
                    SupportedPlatforms = selectedPlatforms,
                    Avatar = avatarImage,
                    GameplayImages = gameplayImages,
                    Cover = coverImage,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                db.Games.Add(game);
            }
            else //Edit
            {
                var gameInDb = db.Games.Include(t => t.Genres).Include(v => v.SupportedPlatforms)
                    .Single(g => g.Id == viewModel.Id);

                Image avatarImageInDb = null;
                Image coverImageInDb = null;

                if (gameInDb.Avatar != null)
                    avatarImageInDb = db.Images.SingleOrDefault(i => i.Id == gameInDb.Avatar.Id);
                if (gameInDb.Cover != null)
                    coverImageInDb = db.Images.SingleOrDefault(j => j.Id == gameInDb.Cover.Id);



                if (avatarImageInDb == null) //Making a new record
                {
                    avatarImageInDb = avatarImage;
                }
                else //Editing old record
                {
                    if (!defaultAvatarUsed) //we replace the old photo only if a new photo has been uploaded.
                    {
                        avatarImageInDb = avatarImage;
                    }
                }


                if (coverImageInDb == null) //Making a new record
                {
                    coverImageInDb = coverImage;
                    //gameInDb.Cover = coverImage;
                }
                else //Editing old record
                {

                    if (!defaultCoverUsed) //we replace the old photo only if a new photo has been uploaded.
                    {
                        coverImageInDb = coverImage;
                    }
                }

                if (viewModel.gameplayOption == GameplayImageOption.MAKE_NEW)
                {
                    foreach (var gameplayImage in gameInDb.GameplayImages.ToList())
                    {
                        db.Images.Remove(db.Images.Single(g => g.Id == gameplayImage.Id));
                    }
                }


                gameInDb.Name = viewModel.Name;
                gameInDb.Genres = selectedGenres;
                gameInDb.Developer = viewModel.Developer;
                gameInDb.Publisher = viewModel.Publisher;
                gameInDb.MetaScore = viewModel.MetaScore;
                gameInDb.UserScore = viewModel.UserScore;
                gameInDb.OnlineCapability = viewModel.OnlineCapability;
                gameInDb.Rating = viewModel.ESRBRating;
                gameInDb.ReleaseDate = viewModel.GetReleaseDate();
                gameInDb.SupportedPlatforms = selectedPlatforms;
                gameInDb.Avatar = avatarImageInDb;
                gameInDb.GameplayImages = (viewModel.gameplayOption==GameplayImageOption.MAKE_NEW)
                    ?gameplayImages
                    :gameInDb.GameplayImages.Concat(gameplayImages).ToList();
                gameInDb.Cover = coverImageInDb;
                gameInDb.UpdatedAt = DateTime.Now;
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Games");
        }
    }
}