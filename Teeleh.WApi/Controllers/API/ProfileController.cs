using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Teeleh.Models;
using Teeleh.Models.ViewModels;
using Teeleh.WApi.Helper;

namespace Teeleh.WApi.Controllers.API
{
    public class ProfileController : ApiController
    {
        private AppDbContext db;

        public ProfileController()
        {
            db = new AppDbContext();
        }

        /// <summary>
        /// It is used for fetching information of a specific user. (User profile page)
        /// </summary>
        /// /// <returns>
        /// 200 : OK |
        /// 400 : Bad Request |
        /// 404 : User Not Found
        /// </returns>
        [HttpPost]
        [Route("api/profile/me")]
        public async Task<IHttpActionResult> GetUserInfo(SessionInfoObject sessionInfoObject)
        {
            if (ModelState.IsValid)
            {
                Session session = await db.Sessions.Include(c => c.User)
                    .SingleOrDefaultAsync(QueryHelper.GetSessionValidationQuery(sessionInfoObject));
                if (session == null)
                {
                    return NotFound();
                }

                User user = session.User;

                var info = new
                {
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.PhoneNumber,
                    user.PSNId,
                    user.XBOXLive
                };

                return Ok(info);
            }

            return BadRequest();
        }
    }
}
