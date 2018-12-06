using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Teeleh.Models;


namespace Teeleh.WApi.Controllers.API
{
    public class GamesController : ApiController
    {
        private AppDbContext db;
        private string localDomain;

        public GamesController()
        {
            db = new AppDbContext();
            localDomain = HttpContext.Current.Request.Url.Host;
        }
       

        [HttpGet]
        [Route("api/games")]
        public IHttpActionResult GetGames()
        {
            
            var games = db.Games.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Image = localDomain+x.Avatar.ImagePath,
                UserScore = x.UserScore,
                MetaScore = x.MetaScore,
                OnlineCapability = x.OnlineCapability,
                Publisher = x.Publisher,
                Developer = x.Developer,
                ReleaseDate = x.ReleaseDate,
                Genres = x.Genres.Select(t => t.Name).ToList(),
                Platforms = x.SupportedPlatforms.Select(p => p.Name).ToList()
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
                    Avatar = localDomain+ gameInDb.Avatar.ImagePath,
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