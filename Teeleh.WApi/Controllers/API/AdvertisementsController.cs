using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Teeleh.Models;

namespace Teeleh.WApi.Controllers
{
    public class AdvertisementsController : ApiController
    {
        private AppDbContext db;

        public AdvertisementsController()
        {
            db = new AppDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetAdvertisements()
        {
            var advertisements = db.Advertisements
                .Select(a => new
                {
                    a.Game.Name,
                    a.Game.Avatar.ImagePath,
                    a.Platform,
                    a.CreatedAt,
                    a.Price
                }).ToList();

            return Ok(advertisements);
        }

        /*public async Task<IHttpActionResult> Create(SessionInfoObject sessionInfo)
        {

        }*/


        /// <summary>
        /// Returns an advertisement in a more detailed manner with given id
        /// </summary>
        /// <returns>200 : Ok |
        /// 404 : Advertisement Not Found |
        /// 400 : Bad Request
        /// </returns>
        [Route("api/advertisements/detail/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Detail(int id)
        {
            var advertise = await db.Advertisements.Include(g=>g.Game)
                .Include(l=>l.Location)
                .Include(p=>p.Platform)
                .Include(i=>i.UserImage)
                .Include(e=>e.GamesToExchange)
                .SingleOrDefaultAsync(a=>a.Id==id);

            if (advertise != null)
            {
                return Ok(advertise);
            }

            return NotFound();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value) { }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/<controller>/5
        public void Delete(int id) { }
    }
}