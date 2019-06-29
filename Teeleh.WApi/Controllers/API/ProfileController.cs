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
                    await db.Sessions.SingleOrDefaultAsync(
                        QueryHelper.GetSessionObjectValidationQuery(sessionInfoObject));
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
                    user.XBOXLive,
                    Location  = new
                    {
                        ProvinceId = user.userProvinceId,
                        Province = user.userProvince?.Name,
                        CityId = user.userCityId,
                        City= user.userCity?.Name
                    }
                };

                return Ok(info);
            }

            return BadRequest();
        }


        /// <summary>
        /// It is used to change the phone number of a user.
        /// </summary>
        /// <returns>
        /// 200 : Ok(smsSent) |
        /// 404 : Not Found (Session info not found) |
        /// 400 : Bad Request |
        /// 409 : User exists with same phone number |
        /// 401 : Use status is not active
        /// </returns>
        [HttpPost]
        [Route("api/profile/edit/phone")]
        public async Task<IHttpActionResult> NewPhoneNumber(PhoneNumberPairDto stringDto)
        {
            if (ModelState.IsValid)
            {

                var sessionInDb = await
                    db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionObjectValidationQuery(stringDto.Session));

                if (sessionInDb != null)
                {
                    var anyOtherUser = db.Users.SingleOrDefault(u => u.PhoneNumber == stringDto.PhoneNumber);

                    if (anyOtherUser == null)
                    {
                        var user = sessionInDb.User;

                        if (user.State == UserState.ACTIVE)
                        {
                            //result = smsSent
                            var result = user.ChangePhoneNumber(stringDto.PhoneNumber);
                            await db.SaveChangesAsync();

                            return Ok(result);
                        }
                        return Unauthorized();
                    }
                     return Conflict();
                }
                return NotFound();
            }
            return BadRequest();
        }


        /// <summary>
        /// It is used to change the Email of a user.
        /// </summary>
        /// <returns>
        /// 200 : Nonce Sent to New Email |
        /// 404 : Not Found (Session info not found) |
        /// 400 : Bad Request |
        /// 409 : User exists with same Email |
        /// 401 : User status is not active
        /// </returns>
        [HttpPost]
        [Route("api/profile/edit/email")]
        public async Task<IHttpActionResult> NewEmail(EmailPairDto pairDto)
        {
            if (ModelState.IsValid)
            {
                var sessionInDb = await
                    db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionObjectValidationQuery(pairDto.Session));
                if (sessionInDb != null)
                {
                    var anyOtherUser = db.Users.SingleOrDefault(u => u.Email == pairDto.Email);

                    if (anyOtherUser == null)
                    {
                        var user = sessionInDb.User;
                        if (user.State == UserState.ACTIVE)
                        {
                            user.ChangeEmail(pairDto.Email);
                            await db.SaveChangesAsync();

                            return Ok();
                        }

                        return Unauthorized();
                    }
                    return Conflict();
                }

                return NotFound();
            }

            return BadRequest();
        }


        /// <summary>
        /// This api evaluate the code sent to the user via their new phone number. (if everything is ok, it will set the new phone number)
        /// </summary>
        /// <returns>
        /// 200 : New Phone Number Replaced |
        /// 404 : Wrong Nonce Entered |
        /// 401 : User Not Found (Wrong Session Info) |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPost]
        [Route("api/profile/edit/phone/validate")]
        public async Task<IHttpActionResult> ValidateNewPhoneNumber(NoncePairDto nonceDto)
        {
            if (ModelState.IsValid)
            {
                var sessionInDb = await
                    db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionObjectValidationQuery(nonceDto.Session));
                if (sessionInDb != null)
                {
                    var user = sessionInDb.User;
                    var result = user.ValidateNewPhoneNumber(nonceDto.Nonce);
                    await db.SaveChangesAsync();
                    if (result)
                        return Ok();
                    else
                    {
                        return NotFound(); //Wrong Nonce
                    }
                }

                return Unauthorized();
            }

            return BadRequest();
        }


        /// <summary>
        /// This api evaluate the code sent to the user via their new Email address. (if everything is ok, it will set the new Email)
        /// </summary>
        /// <param name="nonceDto"></param>
        /// <returns>
        /// 200 : New Email Replaced |
        /// 404 : Wrong Nonce Entered |
        /// 401 : User Not Found (Wrong Session Info) |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPost]
        [Route("api/profile/edit/email/validate")]
        public async Task<IHttpActionResult> ValidateNewEmail(NoncePairDto nonceDto)
        {
            if (ModelState.IsValid)
            {
                var sessionInDb = await
                    db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionObjectValidationQuery(nonceDto.Session));
                if (sessionInDb != null)
                {
                    var user = sessionInDb.User;
                    var result = user.ValidateNewEmail(nonceDto.Nonce);
                    await db.SaveChangesAsync();
                    if (result)
                        return Ok();
                    else
                    {
                        return NotFound(); //Wrong Nonce
                    }
                }

                return Unauthorized();
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
        public async Task<IHttpActionResult> EditProfile(UserInfoDto informationDto)
        {
            if (ModelState.IsValid)
            {
                var sessionInDb = await
                    db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionObjectValidationQuery(informationDto.Session));
                if (sessionInDb != null)
                {
                    var user = sessionInDb.User;
                    user.SetUserInformation(informationDto);
                    await db.SaveChangesAsync();
                    return Ok();
                }

                return Unauthorized();
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
        public async Task<IHttpActionResult> ChangePassword(PasswordPairDto passwordPair)
        {
            if (ModelState.IsValid)
            {
                Session session =
                    await db.Sessions.SingleOrDefaultAsync(
                        QueryHelper.GetSessionObjectValidationQuery(passwordPair.Session));

                if (session == null)
                {
                    return Unauthorized();
                }

                User user = session.User;
                user.ChangePassword(passwordPair.Password);
                await db.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }
    }
}