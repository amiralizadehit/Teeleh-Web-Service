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
                MetaScore = x.MetaScore,
                OnlineCapability = x.OnlineCapability,
                Publisher = x.Publisher,
                Developer = x.Developer,
                ReleaseDate = x.ReleaseDate,
                Genres = x.Genres.Select(t => t.Name).ToList(),
                Platforms = x.SupportedPlatforms.Select(p => p.Name).ToList()
            }).AsEnumerable().Select(v=>new
            {
                Id = v.Id,
                Name = v.Name,
                Image = Url.Content(v.Image),
                UserScore = v.UserScore,
                MetaScore = v.MetaScore,
                OnlineCapability = v.OnlineCapability,
                Publisher = v.Publisher,
                Developer = v.Developer,
                ReleaseDate = v.ReleaseDate,
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