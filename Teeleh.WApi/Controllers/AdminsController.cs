using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Teeleh.Models;
using Teeleh.Models.CustomValidation.Website;
using Teeleh.Models.Panel;
using Teeleh.Models.ViewModels;
using Teeleh.Models.ViewModels.Website_View_Models;
using Teeleh.Utilities;

namespace Teeleh.WApi.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admins
        private AppDbContext db;

        public AdminController()
        {
            db = new AppDbContext();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Login()
        {
            if (Request.Cookies["Login"] != null)
            {
                var adminLogin = new AdminLoginViewModel()
                {
                    Username = Request.Cookies["Login"].Values["Username"]
                };
                return View(adminLogin);
            }

            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Login(AdminLoginViewModel adminLogin)
        {
            if (ModelState.IsValid)
            {
                var admin = db.Admins.SingleOrDefault(a => a.Username == adminLogin.Username
                                                           && a.Password==adminLogin.Password);


                if (admin != null)
                {
                    var secretKey = RandomHelper.RandomString(15);
                    var session = new AdminSession()
                    {
                        Admin = admin,
                        InitMoment = DateTime.Now,
                        SessionKey = secretKey,
                        State = SessionState.Active
                    };
                    Session["SessionKey"] = secretKey;
                    Session["UserId"] = admin.Id;

                    db.AdminSessions.Add(session);
                    db.SaveChanges();

                    if (adminLogin.RememberMe)
                    {
                        HttpCookie cookie = new HttpCookie("Login");
                        cookie.Values.Add("Username", adminLogin.Username);
                        cookie.Expires.AddDays(15);
                        Response.Cookies.Add(cookie);
                    }

                    return RedirectToAction("Edit");
                }

                ModelState.AddModelError(string.Empty, "The user name or password is incorrect.");

                return View(adminLogin);
            }

            return View(adminLogin);
        }

        [SessionTimeout]
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit()
        {
            var userId = (int) Session["UserId"];
            var admin = db.Admins.SingleOrDefault(s => s.Id == userId);

            var adminInfo = new AdminViewModel()
            {
                Id = admin.Id,
                Username = admin.Username,
                Password = admin.Password,
                Email = admin.Email,
                FirstName = admin.FirstName,
                LastName = admin.LastName
            };
            return View(adminInfo);
        }

        [System.Web.Mvc.HttpPost]
        [SessionTimeout]
        public ActionResult Edit(AdminViewModel admin)
        {
            var adminInDb = db.Admins.SingleOrDefault(s => s.Id == admin.Id);

            if (adminInDb!=null)
            {
                adminInDb.Username = admin.Username;
                adminInDb.Password = admin.Password;
                adminInDb.Email = admin.Email;
                adminInDb.FirstName = admin.FirstName;
                adminInDb.LastName = admin.LastName;

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
              return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
    }


    public ActionResult ForgetPassword()
    {
    return View();
}

}
}