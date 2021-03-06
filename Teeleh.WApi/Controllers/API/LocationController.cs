﻿using System.Linq;
using System.Web.Http;
using Teeleh.Models;
using Teeleh.Models.Enums;
using Teeleh.Models.Helper;

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
            var provinces = db.Locations.Where(QueryHelper.GetLocationValidationQuery(null ,LocationType.PROVINCE)).OrderBy(l => l.Name);

            if (provinces.Any())
            {
                return Ok(provinces.Select(QueryHelper.GetLocationQuery()));
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
            var cities = db.Locations.Where(QueryHelper.GetLocationValidationQuery(provinceId,LocationType.CITY)).OrderBy(l => l.Name);
            if (cities.Any())
            {
                return Ok(cities.Select(QueryHelper.GetLocationQuery()));
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
            var cities = db.Locations.Where(QueryHelper.GetLocationValidationQuery(LocationType.CITY)).OrderBy(l => l.Name);

            if (cities.Any())
            {
                Ok(cities.Select(QueryHelper.GetLocationQuery()));
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
            var regions = db.Locations.Where(QueryHelper.GetLocationValidationQuery(cityId,LocationType.REGION)).OrderBy(l => l.Name);

            if (regions.Any())
            {
                return Ok(regions.Select(QueryHelper.GetLocationQuery()));
            }
            return NotFound();
        }
    }
}