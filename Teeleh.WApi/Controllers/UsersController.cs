using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Teeleh.Models;
using Teeleh.Models.ViewModels;
using Teeleh.Utilities;

namespace Teeleh.WApi.Controllers
{
    /// <summary>
    /// This class is used to manage client requests related to users.
    /// </summary>
    public class UsersController : ApiController
    {
        private AppDbContext db;

        public UsersController()
        {
            db = new AppDbContext();
        }


        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return db.Users.ToList();
        }

        [HttpPost]
        [Route("api/users/getuserinfo")]
        public async Task<IHttpActionResult> GetUserInfo(SessionInfoObject sessionInfoObject)
        {
            if (ModelState.IsValid)
            {
                Session session = await db.Sessions.Include(c => c.User)
                    .SingleOrDefaultAsync(q => q.SessionKey == sessionInfoObject.SessionKey);
                if (session == null)
                {
                    return NotFound();
                }

                User user = session.User;

                UserInfoViewModel userInfo = new UserInfoViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserAvatar = user.UserAvatar.AvatarImage,
                    PSNId = user.PSNId,
                    XBOXLive = user.XBOXLive
                };

                return Ok(userInfo);
            }

            return BadRequest();
        }

        ////////////////////////// User Login /////////////////////////////
        /// <summary>
        /// It is used for user login.
        /// </summary>
        /// <returns>200 : Ok (User Logged in Successfully - Session Info Sent) |
        /// 406 : Not Confirmed User |
        /// 400 : Bad Request |
        /// 404 : Not Registered User
        /// </returns>
        [HttpPost]
        [Route("api/users/login")]
        public async Task<IHttpActionResult> Login(LoginViewModel loginInfo)
        {
            if (ModelState.IsValid)
            {
                var hashPassword = HasherHelper.sha256_hash(loginInfo.Password);
                User user = await db.Users.SingleOrDefaultAsync(q =>
                    q.PhoneNumber == loginInfo.PhoneNumber && q.Password == hashPassword);
                if (user == null) //Not Registered User - Use Sign up form
                {
                    return NotFound();
                }

                if (user.State == SessionState.Actived)
                {
                    var sessionKey = RandomHelper.RandomString(32);

                    Session newSession = new Session()
                    {
                        Nonce = null,
                        State = SessionState.Actived,
                        InitMoment = DateTime.Now,
                        ActivationMoment = DateTime.Now,
                        SessionKey = sessionKey,
                        UniqueCode = loginInfo.UniqueCode,
                        User = user
                    };
                    db.Sessions.Add(newSession);

                    await db.SaveChangesAsync();

                    SessionInfoObject sessionIfInfoObject = new SessionInfoObject()
                    {
                        SessionKey = sessionKey,
                        SessionId = newSession.Id,
                    };

                    //OK
                    return Ok(sessionIfInfoObject);
                }

                return Conflict(); //Not confirmed user
            }

            return BadRequest();
        }

        /////////////////////////  User SignUp ////////////////////////////

        /// <summary>
        /// It is used for user sign up with given information.
        /// </summary>
        /// <returns>200 : Ok (User Created - SMS Sent - SessionId Returned) |
        /// 500 : Internal Server Error (SMS Not Sent) |
        /// 400 : Bad Request |
        /// 404 : Already Registered User
        /// </returns>
        [HttpPost]
        [Route("api/users/signup")]
        public async Task<IHttpActionResult> SignUp(UserSignUpSMSViewMode userSignUpSms)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.SingleOrDefaultAsync(q => q.PhoneNumber == userSignUpSms.PhoneNumber);
                Session session = null;
                var randomNounce = RandomHelper.RandomInt(10000, 99999);
                
                if (user == null) //New User
                {
                    user = new User()
                    {
                        FirstName = userSignUpSms.FirstName,
                        LastName = userSignUpSms.LastName,
                        PhoneNumber = userSignUpSms.PhoneNumber,
                        Email = userSignUpSms.Email,
                        Password = HasherHelper.sha256_hash(userSignUpSms.Password),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        State = SessionState.Pending
                    };

                    db.Users.Add(user);

                    session = new Session()
                    {
                        Nonce = randomNounce,
                        State = SessionState.Pending,
                        InitMoment = DateTime.Now,
                        SessionKey = RandomHelper.RandomString(32),
                        UniqueCode = userSignUpSms.UniqueCode,
                        User = user
                    };

                    db.Sessions.Add(session);
                    await db.SaveChangesAsync();

                }
                else
                {
                    if (user.State == SessionState.Pending) //multiple requests
                    {
                        db.Sessions
                            .Where(q => q.UniqueCode == userSignUpSms.UniqueCode)
                            .Where(q => q.State == SessionState.Pending)
                            .ToList()
                            .ForEach(q => q.State = SessionState.Abolished);

                        session = new Session()
                        {
                            Nonce = randomNounce,
                            State = SessionState.Pending,
                            InitMoment = DateTime.Now,
                            SessionKey = RandomHelper.RandomString(32),
                            UniqueCode = userSignUpSms.UniqueCode,
                            User = user
                        };

                        db.Sessions.Add(session);

                        await db.SaveChangesAsync();

                    }
                    else if (user.State == SessionState.Actived) //already registered user - use login form
                    {
                        return Conflict();
                    }
                }

                var receptorPhone = userSignUpSms.PhoneNumber;
                var receptorMail = userSignUpSms.Email;
                var token = randomNounce.ToString();

                if (receptorMail != null) //Mail
                {
                    NotificationHelper.CodeVerificationEmail(token, receptorMail);
                }

                if (NotificationHelper.CodeVerificationSMS_K(token, receptorPhone) != null) //SMS
                {
                    return InternalServerError();
                }

                return Json(new
                {
                    SeesionId = session?.Id ?? -1
                });
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("api/users/signup/email")]
        public async Task<IHttpActionResult> SignUp(UserSignUpEmailViewModel userSignUpEmail)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.SingleOrDefaultAsync(q => q.PhoneNumber == userSignUpEmail.PhoneNumber);
                Session session = null;
                var randomNounce = RandomHelper.RandomInt(10000, 99999);

                if (user == null) //New User
                {
                    user = new User()
                    {
                        FirstName = userSignUpEmail.FirstName,
                        LastName = userSignUpEmail.LastName,
                        PhoneNumber = userSignUpEmail.PhoneNumber,
                        Email = userSignUpEmail.Email,
                        Password = HasherHelper.sha256_hash(userSignUpEmail.Password),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        State = SessionState.Pending
                    };

                    db.Users.Add(user);

                    session = new Session()
                    {
                        Nonce = randomNounce,
                        State = SessionState.Pending,
                        InitMoment = DateTime.Now,
                        SessionKey = RandomHelper.RandomString(32),
                        UniqueCode = userSignUpEmail.UniqueCode,
                        User = user
                    };

                    db.Sessions.Add(session);
                    await db.SaveChangesAsync();

                }
                else
                {
                    if (user.State == SessionState.Pending) //multiple requests
                    {
                        db.Sessions
                            .Where(q => q.UniqueCode == userSignUpEmail.UniqueCode)
                            .Where(q => q.State == SessionState.Pending)
                            .ToList()
                            .ForEach(q => q.State = SessionState.Abolished);

                        session = new Session()
                        {
                            Nonce = randomNounce,
                            State = SessionState.Pending,
                            InitMoment = DateTime.Now,
                            SessionKey = RandomHelper.RandomString(32),
                            UniqueCode = userSignUpEmail.UniqueCode,
                            User = user
                        };

                        db.Sessions.Add(session);

                        await db.SaveChangesAsync();

                    }
                    else if (user.State == SessionState.Actived) //already registered user - use login form
                    {
                        return Conflict();
                    }
                }

                var receptor = userSignUpEmail.Email;
                var token = randomNounce.ToString();
                NotificationHelper.CodeVerificationEmail(token, receptor);
                
                return Json(new
                {
                    SeesionId = session?.Id ?? -1
                });
            }

            return BadRequest();
        }



        ////////////////////// User Forgot Password ///////////////////////


        /// <summary>
        /// It is used to send verification SMS to users who have forgotten their passwords with given phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>200 : Ok (SMS Sent) |
        /// 505 : Internal Server Error (SMS Not Sent) |
        /// 404 : User Not Found |
        /// 400 : Bad Request
        /// </returns>
        [HttpGet]
        [Route("api/users/forgotpassword/{phoneNumber}")]
        public async Task<IHttpActionResult> ForgotPassword(string phoneNumber)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.SingleOrDefaultAsync(q => q.PhoneNumber == phoneNumber);
                var randomNounce = RandomHelper.RandomInt(10000, 99999);

                if (user == null)
                {
                    return NotFound();
                }

                user.ForgetPassCode = randomNounce;

                await db.SaveChangesAsync();

                var receptor = phoneNumber;
                var token = randomNounce.ToString();
                if (NotificationHelper.ForgetPasswordSMS_K(token, receptor) != null)
                {
                    return InternalServerError();
                }

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// It is used to change the old password of users who have forgotten their passwords.
        /// </summary>
        /// <returns>200 : Ok (Password Changed Successfully) |
        /// 404 : User Not Found (Wrong Code Entered) |
        /// 400 : Bad Request
        /// </returns>
        [HttpPut]
        [Route("api/users/forgotpassword")]
        public async Task<IHttpActionResult> ForgotPasswordReset(
            ForgotPasswordVerificationViewModel forgotPasswordVerificationViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.SingleOrDefaultAsync(q =>
                    q.ForgetPassCode == forgotPasswordVerificationViewModel.ForgetPassCode);
                if (user == null)
                {
                    return NotFound(); //Wrong Code
                }

                user.Password = HasherHelper.sha256_hash(forgotPasswordVerificationViewModel.Password);
                user.ForgetPassCode = 0;
                await db.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }


        /// <summary>
        /// It is used to get all active sessions of a user
        /// </summary>
        /// <returns>200 : Ok |
        /// 406 : No Session Found With Given SessionKey |
        /// 400 : Bad Request 
        /// </returns>
        [HttpGet]
        [Route("api/users/getusersessions")]
        public async Task<IHttpActionResult> GetUserSessions(SessionInfoObject sessionInfoObj)
        {
            if (ModelState.IsValid)
            {
                Session session = await db.Sessions.SingleOrDefaultAsync(q =>
                    q.SessionKey == sessionInfoObj.SessionKey && q.State == SessionState.Actived);

                if (session != null)
                {
                    User user = session.User;
                    return Ok(Json(new
                    {
                        user.Sessions
                    }));
                }

                return Conflict();
            }

            return BadRequest();
        }


        /// <summary>
        /// Returns number of users
        /// </summary>
        /// <returns>Number of users</returns>
        [Route("api/users/count")]
        [HttpGet]
        public int GetCount()
        {
            return db.Users.Count();
        }
    }
}

/* /// <summary>
         /// Further information of user is sent to this end-point
         /// </summary>
         /// <param name="SessionKey"></param>
         /// <param name="FirstName"></param>
         /// <param name="LastName"></param>
         /// <param name="Password"></param>
         /// <returns>Http Status Code</returns>
         [HttpPost]
         [Route("api/users/userfurtherinfo")]
         public async Task<IHttpActionResult> UserInfo(UserFurtherInfoViewModel userFurtherInfo)
         {
             if (ModelState.IsValid)
             {
                 Session session =
                     await db.Sessions.SingleOrDefaultAsync(f =>
                         f.SessionKey == userFurtherInfo.SessionKey && f.State == SessionState.Actived);
                 if (session != null)
                 {
                     User userInDb = session.User;
                     userInDb.FirstName = userFurtherInfo.FirstName;
                     userInDb.LastName = userFurtherInfo.LastName;
                     userInDb.Password = HasherHelper.sha256_hash(userFurtherInfo.Password);
                     userInDb.Email = userFurtherInfo.Email;
                     userInDb.IsDeleted = false;
                     userInDb.UpdatedAt = DateTime.Now;
 
                     await db.SaveChangesAsync();
                     return Json(new
                     {
                         HttpStatusCode = HttpStatusCode.OK
                     });
                 }
 
                 return Json(new
                 {
                     HttpStatusCode = HttpStatusCode.NotAcceptable
                 });
             }
 
             return Json(new
             {
                 HttpStatusCode = HttpStatusCode.BadRequest
             });
         }*/