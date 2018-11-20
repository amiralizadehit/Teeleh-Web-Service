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

        /////////////////////////////////////////////////////////////// Province ////////////////////////////////////////////////////////////////////

        /// <summary>
        /// This endpoint returns a list of provinces.
        /// </summary>
        /// <returns>200 : sent 
        /// </returns>
        [HttpGet]
        [Route("api/locations/getprovince")]
        public IHttpActionResult GetProvince()
        {
            var provinces = db.Locations.Where(l => l.Parent == null && l.Type == Location.LocationType.PROVINCE);

            if (provinces.Any())
            {
                return Ok(provinces.Select(b => new
                {
                    Id = b.Id,
                    Name = b.Name
                }));
            }

            return NotFound();
        }

        /////////////////////////////////////////////////////////////// City /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// This endpoint returns a list of cities inside a province with given Id.
        /// </summary>
        /// <returns>200 : sent 
        /// </returns>
        [HttpGet]
        [Route("api/locations/getcity/{provinceId}")]
        public IHttpActionResult GetCity(int provinceId)
        {
            var cities = db.Locations.Where(c => c.ParentId == provinceId && c.Type == Location.LocationType.CITY);
            if (cities.Any())
            {
                return Ok(cities.Select(b => new
                {
                    Id = b.Id,
                    Name = b.Name
                }));
            }

            return NotFound();
        }

        /// <summary>
        /// This endpoint returns a list of cities which have no provinces.
        /// </summary>
        /// <returns>200 : sent 
        /// </returns>
        [HttpGet]
        [Route("api/locations/getcity")]
        public IHttpActionResult GetCity()
        {
            var cities = db.Locations.Where(c =>
                c.Type == Location.LocationType.CITY);

            if (cities.Any())
            {
                Ok(cities.Select(b => new
                {
                    Id = b.Id,
                    Name = b.Name
                }));
            }

            return NotFound();
        }

        //////////////////////////////////////////////////////////////////// Region ////////////////////////////////////////////////////////////////

        /// <summary>
        /// This endpoint returns a list of regions inside a city with given Id.
        /// </summary>
        /// <returns>200 : sent 
        /// </returns>
        [HttpGet]
        [Route("api/locations/getregion/{cityId}")]
        public IHttpActionResult GetRegion(int cityId)
        {
            var regions = db.Locations.Where(g =>
                    g.ParentId == cityId && g.Type == Location.LocationType.REGION);

            if (regions.Any())
            {
                return Ok(regions.Select(b => new
                {
                    Id = b.Id,
                    Name = b.Name
                }));
            }
            return NotFound();
        }
    }
}