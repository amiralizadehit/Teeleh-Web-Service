using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Teeleh.Models;
using Teeleh.Models.ViewModels;

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


        public ActionResult Index()
        {
            var games = db.Games.Include(p => p.SupportedPlatforms).Include(g => g.Genres).Include(a=>a.Avatar).ToList();

            return View(games);
        }

        public ActionResult Edit(int id)
        {
            var viewModel = new GameFormViewModel();

            var game = db.Games.Include(p=>p.SupportedPlatforms).Include(g=>g.Genres).SingleOrDefault(g => g.Id == id);

            if (game == null)
            {
                return HttpNotFound();
            }
            else
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
                viewModel.UserScore = game.UserScore;
                viewModel.Id = id;

                return View("Create", viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var gameToDelete = db.Games.Single(g => g.Id == id);
            db.Games.Remove(gameToDelete);
            db.SaveChanges();

            return RedirectToAction("Index", "Games");
        }


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

            viewModel.Platforms = new MultiSelectList(platforms,"PlatformId","PlatformName");
            //viewModel.SelectedPlatforms = new[] {Platform.PC,Platform.Switch,Platform.Android};
            viewModel.Genres = new MultiSelectList(genres,"GenreId","GenreName");
            //viewModel.SelectedGenres = new[] {1, 3, 4};
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

            var avatarPhotoFileExtension = Path.GetExtension(viewModel.ImageFile.FileName);
            var avatarPhotoFileName = viewModel.Name + "_" + "Avatar"+"_"+DateTime.Now.ToString("yymmssfff")+avatarPhotoFileExtension;
            var avatarPhotoFilePath = "~/Image/" + avatarPhotoFileName;
            avatarPhotoFileName = Path.Combine(Server.MapPath("~/Image/"), avatarPhotoFileName);
            viewModel.ImageFile.SaveAs(avatarPhotoFileName);

            var selectedGenres = db.Genres.Where(g => viewModel.SelectedGenres.Contains(g.Id)).ToList();
            var selectedPlatforms = db.Platforms.Where(p => viewModel.SelectedPlatforms.Contains(p.Id)).ToList();
            if (viewModel.Id == -1)
            {
                var avatarImage = new Image()
                {
                    Name = viewModel.Name,
                    ImagePath = avatarPhotoFilePath,
                    Type = Image.ImageType.AVATAR,
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
                    ReleaseDate = viewModel.GetReleaseDate(),
                    SupportedPlatforms = selectedPlatforms,
                    Avatar = avatarImage,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                db.Games.Add(game);
                db.Images.Add(avatarImage);
            }
            else
            {
                var gameInDb = db.Games.Include(i=>i.Avatar).Include(g=>g.Genres).Include(p=>p.SupportedPlatforms).Single(g => g.Id == viewModel.Id);
               
                    var imageInDb = db.Images.SingleOrDefault(i => i.Id == gameInDb.Avatar.Id);

                    imageInDb.Name = viewModel.Name;
                    imageInDb.ImagePath = avatarPhotoFilePath;
                    imageInDb.Type = Image.ImageType.AVATAR;
                    imageInDb.UpdatedAt = DateTime.Now;

                    gameInDb.Name = viewModel.Name;
                    gameInDb.Genres = selectedGenres;
                    gameInDb.Developer = viewModel.Developer;
                    gameInDb.Publisher = viewModel.Publisher;
                    gameInDb.MetaScore = viewModel.MetaScore;
                    gameInDb.UserScore = viewModel.UserScore;
                    gameInDb.OnlineCapability = viewModel.OnlineCapability;
                    gameInDb.ReleaseDate = viewModel.GetReleaseDate();
                    gameInDb.SupportedPlatforms = selectedPlatforms;
                    gameInDb.Avatar = imageInDb;
                    gameInDb.UpdatedAt = DateTime.Now;

                }
            db.SaveChanges();

            return RedirectToAction("Index","Games");

        }

        /// <summary>
        /// It is used for retrieving game information
        /// </summary>
        /// <returns>200 : Ok (game info returned) |
        /// 404 : Game Not Found
        /// </returns>
        /*[System.Web.Mvc.HttpGet]
        public async Task<IHttpActionResult> Details(int id)
        {
            Game game = await db.Games.Include(q => q.Genres).SingleOrDefaultAsync(g => g.Id == id);

            if (game != null)
            {
                return Ok(game);
            }

            return NotFound();
        }*/

        /*[System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> Save(Game game)
        {
            if (!ModelState.IsValid)
            {

            }
        }*/

    }
}
