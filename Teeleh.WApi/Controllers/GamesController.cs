using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Teeleh.Models;
using Teeleh.Models.CustomValidation.Website;
using Teeleh.Models.Enums;
using Teeleh.Models.ViewModels;
using Teeleh.Models.ViewModels.Website_View_Models;
using Teeleh.Utilities;

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
            var games = db.Games.ToList();

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
            viewModel.UserScore = game.UserScore;
            viewModel.Id = id;

            return View("Create", viewModel);
        }

        [SessionTimeout]
        public ActionResult Delete(int id)
        {
            var gameToDelete = db.Games.Single(g => g.Id == id);
            db.Games.Remove(gameToDelete);
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

            var gameName = viewModel.Name.Replace("?", "").Replace(":", "").TrimEnd().TrimStart();
            var folderRandomIndex = RandomHelper.RandomInt(0, 10000);
            bool defaultCoverUsed = false;
            bool defaultAvatarUsed = false;

            string avatarPhotoFilePath = "";
            if (viewModel.AvatarImage != null)
            {
                var fileRandomIndex = RandomHelper.RandomInt(0, 10000);

                var avatarPhotoFileExtension = Path.GetExtension(viewModel.AvatarImage.FileName);

                Directory.CreateDirectory(Server.MapPath("~/Image/Games/" + folderRandomIndex));

                var avatarPhotoFileName = fileRandomIndex + DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") +
                                          avatarPhotoFileExtension;
                avatarPhotoFilePath = "/Image/Games/" + folderRandomIndex + "/" + avatarPhotoFileName;
                avatarPhotoFileName =
                    Path.Combine(Server.MapPath("~/Image/Games/" + folderRandomIndex + "/"), avatarPhotoFileName);
                viewModel.AvatarImage.SaveAs(avatarPhotoFileName);
            }
            else
            {
                //Default
                avatarPhotoFilePath = "/Image/Games/Default/Default.jpg";
                defaultAvatarUsed = true;
            }

            string coverPhotoFilePath = "";
            if (viewModel.AvatarImage != null)
            {
                var fileRandomIndex = RandomHelper.RandomInt(0, 10000);
                Directory.CreateDirectory(Server.MapPath("~/Image/Games/" + folderRandomIndex));
                var coverPhotoFileExtension = Path.GetExtension(viewModel.CoverImage.FileName);
                var coverPhotoFileName = fileRandomIndex + DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") +
                                         coverPhotoFileExtension;
                coverPhotoFilePath = "/Image/Games/" + folderRandomIndex + "/" + coverPhotoFileName;
                coverPhotoFileName =
                    Path.Combine(Server.MapPath("~/Image/Games/" + folderRandomIndex + "/"), coverPhotoFileName);
                viewModel.CoverImage.SaveAs(coverPhotoFileName);
            }
            else
            {
                //Here we set the default photo for cover image - coverPhotoFilePath= ...
                coverPhotoFilePath = "/Image/Games/Default/DefaultCover.jpg";
                defaultCoverUsed = true;
            }


            var selectedGenres = db.Genres.Where(g => viewModel.SelectedGenres.Contains(g.Id)).ToList();
            var selectedPlatforms = db.Platforms.Where(p => viewModel.SelectedPlatforms.Contains(p.Id)).ToList();

            if (viewModel.Id == -1) //New
            {
                var avatarImage = new Image()
                {
                    Name = viewModel.Name,
                    ImagePath = avatarPhotoFilePath,
                    Type = ImageType.AVATAR,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                var coverImage = new Image()
                {
                    Name = viewModel.Name,
                    ImagePath = coverPhotoFilePath,
                    Type = ImageType.COVER,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

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
                    Cover = coverImage,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                db.Games.Add(game);
                db.Images.Add(avatarImage);
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
                    avatarImageInDb = new Image()
                    {
                        Name = viewModel.Name,
                        ImagePath = avatarPhotoFilePath,
                        Type = ImageType.AVATAR,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    //gameInDb.Avatar = avatarImage;
                }
                else //Editing old record
                {
                    if (!defaultAvatarUsed) //we replace the old photo only if a new photo has been uploaded.
                    {
                        avatarImageInDb.Name = viewModel.Name;
                        avatarImageInDb.ImagePath = avatarPhotoFilePath;
                        avatarImageInDb.Type = ImageType.AVATAR;
                        avatarImageInDb.UpdatedAt = DateTime.Now;
                    }
                }


                if (coverImageInDb == null) //Making a new record
                {
                    coverImageInDb = new Image()
                    {
                        Name = viewModel.Name,
                        ImagePath = coverPhotoFilePath,
                        Type = ImageType.COVER,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    //gameInDb.Cover = coverImage;
                }
                else //Editing old record
                {

                    if (!defaultCoverUsed) //we replace the old photo only if a new photo has been uploaded.
                    {
                        coverImageInDb.Name = viewModel.Name;
                        coverImageInDb.ImagePath = coverPhotoFilePath;
                        coverImageInDb.Type = ImageType.COVER;
                        coverImageInDb.UpdatedAt = DateTime.Now;
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
                gameInDb.Cover = coverImageInDb;
                gameInDb.UpdatedAt = DateTime.Now;
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Games");
        }
    }
}