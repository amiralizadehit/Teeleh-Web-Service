using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Teeleh.Models;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Models.Helper;

namespace Teeleh.WApi.Controllers.API
{
    public class PSNAccountRequestController : ApiController
    {
        private AppDbContext db;

        public PSNAccountRequestController()
        {
            db = new AppDbContext();
        }


        /// <summary>
        /// This endpoint creates a psn account request with the given information.
        /// </summary>
        /// <returns>200 : Request Created |
        /// 401 : Session info not found |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPost]
        [Route("api/requests_accounts/psn/create")]
        public async Task<IHttpActionResult> Create(PSNAccountRequestCreateDto requestCreate)
        {
            if (ModelState.IsValid)
            {
                var session = await
                    db.Sessions.SingleOrDefaultAsync(
                        QueryHelper.GetSessionObjectValidationQuery(requestCreate.Session));
                if (session != null)
                {
                    var user = session.User;

                    var games = db.Games.Where(g => requestCreate.Games.Contains(g.Id)).ToList();

                    var newRequest = new PSNAccountRequest()
                    {
                        Games = games,
                        Region = requestCreate.Region,
                        Capacity = requestCreate.Capacity,
                        HasPlus = requestCreate.HasPlus,
                        Type = requestCreate.Type,
                        User = user,
                        MaxPrice = requestCreate.MaxPrice,
                        MinPrice = requestCreate.MinPrice,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    db.PSNAccountRequests.Add(newRequest);

                    await db.SaveChangesAsync();

                    return Ok(newRequest.Id);
                }

                return Unauthorized();
            }
            return BadRequest();
        }
    }
}