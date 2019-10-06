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
using Teeleh.Models.Helper;
using Teeleh.Utilities;

namespace Teeleh.WApi.Controllers.API
{
    public class UserPhoneNumberValidatorController : ApiController
    {
        private AppDbContext db;

        public UserPhoneNumberValidatorController()
        {
            db = new AppDbContext();
        }

        /// <summary>
        /// This endpoint creates a new validator for a new phone number.
        /// </summary>
        /// <returns>200 : Validator Created (Nonce Returned) |
        /// 401 : Session info not found |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPost]
        [Route("api/phone/validation/create")]
        public async Task<IHttpActionResult> Create(PhoneNumberValidatorDto phoneNumberValidatorDto)
        {
            if (ModelState.IsValid)
            {
                var session = await
                    db.Sessions.SingleOrDefaultAsync(
                        QueryHelper.GetSessionObjectValidationQuery(phoneNumberValidatorDto.Session));
                if (session != null)
                {
                    var smsSent = true;
                    int nonce = RandomHelper.RandomInt(10000, 99999);
                    var validator = new UserPhoneNumberValidator()
                    {
                        UserId = session.User.Id,
                        TargetNumber = phoneNumberValidatorDto.PhoneNumber,
                        SecurityToken = HasherHelper.sha256_hash(nonce.ToString()),
                        IsValidated = false,
                        CreatedAt = DateTime.Now
                    };
                    db.UserPhoneNumberValidators.Add(validator);
                    await db.SaveChangesAsync();
                    if (MessageHelper.SendSMS_K(nonce.ToString(), phoneNumberValidatorDto.PhoneNumber,
                            MessageHelper.SMSMode.VERIFICATION) != null)
                    {
                        smsSent = false;
                    }

                    return Ok();
                }

                return Unauthorized();
            }

            return BadRequest();
        }

        /// <summary>
        /// This endpoint validate a nonce in validator list.
        /// </summary>
        /// <returns>200 : Successfully Validated |
        /// 401 : Session info not found |
        /// 404 : Validator Not Found (Wrong Nonce) |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPost]
        [Route("api/phone/validation/validate")]
        public async Task<IHttpActionResult> Validate(NoncePairDto noncePair)
        {
            if (ModelState.IsValid)
            {
                var session = await
                    db.Sessions.SingleOrDefaultAsync(
                        QueryHelper.GetSessionObjectValidationQuery(noncePair.Session));
                if (session != null)
                {
                    var validator = db.UserPhoneNumberValidators.Where(v => v.UserId == session.User.Id).OrderByDescending(v => v.CreatedAt)
                        .First();

                    if (validator.SecurityToken == HasherHelper.sha256_hash(noncePair.Nonce.ToString()))
                    {
                        validator.IsValidated = true;
                        validator.ValidatedAt = DateTime.Now;
                        return Ok();
                    }

                    return NotFound(); //Wrong Nonce
                }
                return Unauthorized();
            }
            return BadRequest();
        }


    }
}