using System.Collections.Generic;
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

        [System.Web.Http.HttpGet]
        public IEnumerable<Advertisement> GetAdvertisements()
        {
            var advertisements = db.Advertisements.ToList();
            return advertisements;
        }

        public async Task<IHttpActionResult> Detail(int Id)
        {
            if (ModelState.IsValid)
            {
                var advertise = await db.Advertisements.SingleOrDefaultAsync(c => c.Id == Id);
                if (advertise == null)
                    return NotFound();
                else
                    return Ok(advertise);
            }

            return BadRequest();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}