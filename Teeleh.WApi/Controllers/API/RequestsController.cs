using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Teeleh.Models;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Models.ViewModels;
using Teeleh.WApi.Helper;

namespace Teeleh.WApi.Controllers.API
{
    public class RequestsController : ApiController
    {
        private AppDbContext db;

        public RequestsController()
        {
            db = new AppDbContext();
        }


        /// <summary>
        /// This endpoint returns a list of all requests.
        /// </summary>
        /// <returns>200 : sent
        /// </returns>
        [HttpGet]
        [Route("api/requests")]
        public IHttpActionResult GetRequests()
        {
            var requests = db.Requests.Select(QueryHelper.GetRequestQuery()).ToList();

            return Ok(requests);
        }

        /// <summary>
        /// This endpoint returns a list of requests owned by a user specified by given session information.
        /// </summary>
        /// <returns>200 : sent |
        /// 400 : Bad Request |
        /// 401 : Session info not found
        /// </returns>
        [HttpPost]
        [Route("api/requests/me")]
        public async Task<IHttpActionResult> GetRequests(SessionInfoObject sessionInfoObject)
        {
            if (ModelState.IsValid)
            {
                var sessionInDb = await
                    db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionValidationQuery(sessionInfoObject));
                if (sessionInDb != null)
                {
                    var user = sessionInDb.User;
                    var requests = db.Requests

                        .Where(QueryHelper.GetRequestValidationQuery()).Where(c => c.User.Id == user.Id)
                        .Select(QueryHelper.GetRequestQuery()).ToList();
                    return Ok(requests);

                }

                return Unauthorized();
            }

            return BadRequest();
        }

        /// <summary>
        /// This endpoint creates a request with the given information.
        /// </summary>
        /// <returns>200 : Request Created |
        /// 401 : Session info not found |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPost]
        [Route("api/requests/create")]
        public async Task<IHttpActionResult> Create(RequestCreateDto requestCreate)
        {
            if (ModelState.IsValid)
            {
                var session = await
                    db.Sessions.SingleOrDefaultAsync(
                        QueryHelper.GetSessionValidationQuery(requestCreate.SeesionInfoObject));
                if (session != null)
                {
                    var user = session.User;

                    var selectedPlatforms = db.Platforms.Where(g => requestCreate.SelectedPlatforms.Contains(g.Id)).ToList();

                    var new_request = new Request()
                    {
                        GameId = requestCreate.GameId,
                        FilterType = (FilterType)requestCreate.FilterType,
                        ReqMode = (RequestMode)requestCreate.ReqMode,
                        LocationCityId = requestCreate.LocationCity,
                        LocationProvinceId = requestCreate.LocationProvince,
                        LocationRegionId = requestCreate.LocationRegion,
                        User = user,
                        MaxPrice = requestCreate.MaxPrice,
                        MinPrice = requestCreate.MinPrice,
                        Platforms = selectedPlatforms,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    db.Requests.Add(new_request);
                    db.SaveChanges();

                    return Ok(new_request.Id);
                }

                return Unauthorized();
            }

            return BadRequest();
        }


        //[HttpPost]
        //[Route("api/requests/edit")]
        //public async Task<IHttpActionResult> Edit()


        /// <summary>
        /// This endpoint deletes a request with given id.
        /// </summary>
        /// <returns>200 : Ok |
        /// 401 : Session info not found |
        /// 400 : Bad Request |
        /// 404 : Request not found
        /// </returns>
        [HttpPost]
        [Route("api/requests/delete")]
        public async Task<IHttpActionResult> Delete(IDPairDto pair)
        {
            if (ModelState.IsValid)
            {
                var sessionInDb = await
                    db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionValidationQuery(pair.session));
                if (sessionInDb != null)
                {
                    var requestInDb = db.Requests.SingleOrDefault(a => a.Id == pair.Id);
                    if (requestInDb != null)
                    {
                        requestInDb.IsDeleted = true;
                        await db.SaveChangesAsync();
                        return Ok();
                    }

                    return NotFound();
                }

                return Unauthorized();
            }

            return BadRequest();
        }
    }
}
