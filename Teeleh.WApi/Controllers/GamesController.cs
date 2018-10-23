using System.Collections.Generic;
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


        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        /// <returns>List of users</returns>
        [System.Web.Mvc.HttpGet]
        public IEnumerable<Game> GetGames()
        {
            return db.Games.ToList();
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

            viewModel.Platforms = new MultiSelectList(platforms,"PlatformId","PlatformName");
            viewModel.SelectedPlatforms = new[] {Platform.PC,Platform.Switch,Platform.Android};
            viewModel.Genres = new MultiSelectList(genres,"GenreId","GenreName");
            viewModel.SelectedGenres = new[] {1, 3, 4};
            
            return View(viewModel);
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
