using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Teeleh.Models;

namespace Teeleh.WApi.Controllers
{
    public class GamesController : ApiController
    {
        private AppDbContext db;

        public GamesController()
        {
            db = new AppDbContext();
        }

        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
            return db.Games.ToList();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Detail(int id)
        {
            var game = db.Games.Include(q=>q.Genres).Include(c=>c.)
        }

    }
}
