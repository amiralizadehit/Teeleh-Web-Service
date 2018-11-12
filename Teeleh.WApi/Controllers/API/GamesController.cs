using System.Linq;
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

}
}