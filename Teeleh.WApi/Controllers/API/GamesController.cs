using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Teeleh.Models;
using Teeleh.WApi.Helper;


namespace Teeleh.WApi.Controllers.API
{
    public class GamesController : ApiController
    {
        private AppDbContext db;
       

        public GamesController()
        {
            db = new AppDbContext();
         
        }
       

        [HttpGet]
        [Route("api/games")]
        public IHttpActionResult GetGames()
        {
            
            var games = db.Games.Select(QueryHelper.GetGameQuery()).ToList();
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
                    Avatar = QueryHelper.GetLocalDomain() + gameInDb.Avatar.ImagePath,
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