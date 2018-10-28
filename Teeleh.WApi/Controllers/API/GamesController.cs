using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Teeleh.Models;

namespace Teeleh.WApi.Controllers.API
{
    public class GamesController : ApiController
    {
        private AppDbContext db;

        public GamesController()
        {
            db = new AppDbContext();
        }

        public IHttpActionResult GetGames()
        {
            var games = db.Games.Include(p => p.SupportedPlatforms).Include(g => g.Genres).Include(a => a.Avatar).Select(x=>new
            {
                x.Name,
                x.ReleaseDate,
                x.Genres,
                x.OnlineCapability,
                x.Developer,
                x.Publisher,
                x.Avatar,
                x.MetaScore,
                x.UserScore,
            });

            return Ok(games);
        }

}
}