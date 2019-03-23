using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Teeleh.Models;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Models.Helper;
using Teeleh.Models.ViewModels;

namespace Teeleh.WApi.Controllers
{
    /// <summary>
    /// This class is used to manage sessions and authentication
    /// </summary>
    public class SessionsController : ApiController
    {
        AppDbContext db = new AppDbContext();


        /// <summary>
        /// Returns a list of all sessions.
        /// </summary>
        /// <returns>List of all sessions</returns>
        [HttpGet]
        public IHttpActionResult GetSessions()
        {

            return Ok(db.Sessions.Select(QueryHelper.GetSessionQuery()).ToList());
        }


        /// <summary>
        /// Actives created session for user with given phoneNumber and nonce then returns SessionId and SessionKey.
        /// </summary>
        /// <returns>200 : Ok (User Confirmed Successfully - Session Info Sent) |
        /// 404 : No Session Found (Wrong Nounce Entered) |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPut]
        [Route("api/sessions/active")]
        public async Task<IHttpActionResult> Active(PendingSessionViewModel pendingSession)
        {
            if (ModelState.IsValid)
            {
                var session =
                    await db
                        .Sessions
                        .Where(QueryHelper.GetPendingSessionQuery(pendingSession))
                        .SingleOrDefaultAsync();
                if (session == null)
                {
                    return NotFound(); //Wrong Nounce Entered
                }

                session.User.State = SessionState.ACTIVE;
                session.User.UpdatedAt = DateTime.Now;
                session.ActivationMoment = DateTime.Now;
                session.State = SessionState.ACTIVE;

                await db.SaveChangesAsync();

                SessionInfoObject sessionInfo = new SessionInfoObject()
                {
                    SessionKey = session.SessionKey,
                    SessionId = session.Id
                };

                return Ok(sessionInfo);
            }

            return BadRequest();
        }

        /// <summary>
        /// Deactives active session with given SessionId and SessionKey - User Logout
        /// </summary>
        /// <returns>200 : Ok (Session Deactivated Successfully) |
        /// 404 : No Session Found (Wrong SessionId or SessionKey) |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPut]
        [Route("api/sessions/deactive")]
        public async Task<IHttpActionResult> Deactive(SessionInfoObject sessionInfo)
        {
            if (ModelState.IsValid)
            {
                var session =
                    await db
                        .Sessions
                        .Where(QueryHelper.GetSessionValidationQuery(sessionInfo))
                        .SingleOrDefaultAsync();
                if (session == null)
                {
                    return NotFound();
                }

                session.DeactivationMoment = DateTime.Now;
                session.State = SessionState.DEACTIVE;
                await db.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }


        /// <summary>
        /// You can set Firebase Cloud Messaging token using this endpoint with given session information.
        /// </summary>
        /// <returns>200 : Ok (FCM token updated) |
        /// 404 : No Session Found (Wrong SessionId or SessionKey) |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPost]
        [Route("api/sessions/fcm/set")]
        public async Task<IHttpActionResult> SetFCMToken(FCMTokenDto fcmFcmTokenDto)
        {
            if (ModelState.IsValid)
            {
                Session session = await db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionValidationQuery(fcmFcmTokenDto.session));

                if (session != null)
                {
                    session.FCMToken = fcmFcmTokenDto.token;
                    db.SaveChanges();
                    return Ok();
                }

                return NotFound();
            }
            return BadRequest();
        }
    }
}