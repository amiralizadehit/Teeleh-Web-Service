using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Teeleh.Models;
using Teeleh.Models.Helper;
using Teeleh.Models.ViewModels;

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
        /// 401 : Session info not found
        /// </returns>
        [HttpPost]
        [Route("api/profile/me")]
        public async Task<IHttpActionResult> GetUserInfo(SessionInfoObject sessionInfoObject)
        {
            if (ModelState.IsValid)
            {
                Session session =
                    await db.Sessions.SingleOrDefaultAsync(QueryHelper.GetUserValidationQuery(sessionInfoObject));
                if (session == null)
                {
                    return Unauthorized();
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

        /// <summary>
        /// It is used for updating user's profile information
        /// </summary>
        /// /// <returns>
        /// 200 : OK |
        /// 400 : Bad Request |
        /// 401 : Session info not found
        /// </returns>
        [HttpPost]
        [Route("api/profile/edit")]
        public async Task<IHttpActionResult> EditProfile(UserInfoViewModel userInfo)
        {
            if (ModelState.IsValid)
            {
                Session session =
                    await db.Sessions.SingleOrDefaultAsync(QueryHelper.GetUserValidationQuery(userInfo.SessionInfo));
                if (session == null)
                {
                    return Unauthorized();
                }

                User user = session.User;
                user.FirstName = userInfo.FirstName;
                user.LastName = userInfo.LastName;
                user.PSNId = userInfo.PSNId;
                user.XBOXLive = userInfo.XBOXLive;

                await db.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }
        /// <summary>
        /// It is used for changing the password of a user.
        /// </summary>
        /// /// <returns>
        /// 200 : OK |
        /// 400 : Bad Request |
        /// 401 : Session info not found
        /// </returns>
        [HttpPost]
        [Route("api/profile/edit/password")]
        public async Task<IHttpActionResult> ChangePassword(UserPasswordViewModel userpass)
        {
            if (ModelState.IsValid)
            {
                Session session =
                    await db.Sessions.SingleOrDefaultAsync(QueryHelper.GetUserValidationQuery(userpass.SessionInfo));

                if (session == null)
                {
                    return Unauthorized();
                }

                User user = session.User;
                var newPassword = Utilities.HasherHelper.sha256_hash(userpass.Password);
                user.Password = newPassword;
                await db.SaveChangesAsync();

                return Ok();

            }

            return BadRequest();
        }
    }
}
