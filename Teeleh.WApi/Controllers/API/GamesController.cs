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
                
            }).AsEnumerable().Select(v=>new
            {
                Id = v.Id,
                Name = v.Name,
                Image = Url.Content(v.Image),
            }).ToList();

            return Ok(games);
        }

}
}