using System.Linq;
using System.Web.Http;
using Teeleh.Models;

namespace Teeleh.WApi.Controllers.API
{
    public class LocationController : ApiController
    {
        private readonly AppDbContext db;

        public LocationController()
        {
            db = new AppDbContext();
        }

        [HttpGet]
        [Route("api/locations/getprovince")]
        public IHttpActionResult GetProvince()
        {
            var provinces = db.Locations.Where(l=>l.Parent==null && l.Type==Location.LocationType.PROVINCE).Select(b=>new
            {
                Id = b.Id,
                Name = b.Name
            });

            return Ok(provinces);
        }

        [HttpGet]
        [Route("api/locations/getcity/{provinceId}")]
        public IHttpActionResult GetCity(int provinceId)
        {
            var cities = db.Locations.Where(c => 
                c.ParentId == provinceId && c.Type==Location.LocationType.CITY)
                .Select(b => new
            {
                Id = b.Id,
                Name = b.Name
            });
            return Ok(cities);
        }

        [HttpGet]
        [Route("api/locations/getcity")]
        public IHttpActionResult GetCity()
        {
            var cities = db.Locations.Where(c=>
                c.Type==Location.LocationType.CITY)
                .Select(b => new
            {
                Id = b.Id,
                Name = b.Name
            });
            return Ok(cities);
        }

        [HttpGet]
        [Route("api/locations/getregion/{cityId}")]
        public IHttpActionResult GetRegion(int cityId)
        {
            var regions = db.Locations.Where(g => 
                g.ParentId == cityId && g.Type==Location.LocationType.REGION)
                .Select(b => new
            {
                Id = b.Id,
                Name = b.Name
            });
            return Ok(regions);
        }




            
            

    }
}
