using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Teeleh.Models;


namespace Teeleh.WApi.Controllers.API
{
    public class GamesController : ApiController
    {
        private AppDbContext db = new AppDbContext();

       

        [HttpGet]
        [Route("api/games")]
        public IHttpActionResult GetGames()
        {
            var games = db.Games.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Avatar.ImagePath,
                UserScore = x.UserScore,
                Genres = x.Genres.Select(t => t.Name).ToList(),
                Platforms = x.SupportedPlatforms.Select(p => p.Name).ToList()
            }).AsEnumerable().Select(v=>new
            {
                Id = v.Id,
                Name = v.Name,
                Image = Url.Content(v.Image),
                UserScore = v.UserScore,
                Genres = v.Genres,
                Platforms = v.Platforms
            }).ToList();
            return Ok(games);
        }
        [HttpGet]
        [Route("api/games/{id}")]
        public async Task<IHttpActionResult> Detail(int id)
        {
            var gameInDb = await db.Games.SingleOrDefaultAsync(g => g.Id == id);

            if (gameInDb != null)
            {
                var game = new
                {
                    gameInDb.Name,
                    Avatar = Url.Content(gameInDb.Avatar.ImagePath),
                    gameInDb.Developer,
                    gameInDb.MetaScore,
                    gameInDb.UserScore,
                    gameInDb.OnlineCapability,
                    gameInDb.Publisher,
                    gameInDb.ReleaseDate,
                    Genres = gameInDb.Genres.Select(f=>f.Name),
                    SupportedPlatforms = gameInDb.SupportedPlatforms.Select(p=>p.Name),
                };
                return Ok(game);
            }

            return NotFound();
        }
}
}