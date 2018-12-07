using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Teeleh.Models;
using Teeleh.Models.ViewModels.Website_View_Models;

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

        [System.Web.Http.HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult Login(AdminLoginViewModel adminLogin)
        {
            if (ModelState.IsValid)
            {
                var admin = db.Admins.SingleOrDefault(a => a.Username == adminLogin.Username
                                                           && a.Password == adminLogin.Password);
                if (admin != null)
                {
                    RedirectToAction("Edit", "Admin", adminLogin);
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password is incorrect.");
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }


            return RedirectToAction("Index");
        }

        //[Authorize]
        public ActionResult Edit(int id)
        {
            var admin = db.Admins.SingleOrDefault(a => a.Id == id);

            var adminInfo = new AdminLoginViewModel()
            {
                Id = id,
                Username =  admin.Username,
                Password = admin.Password,
                Email = admin.Email,
                FirstName = admin.FirstName
            };
            return View(adminInfo);

        }


        public ActionResult ForgetPassword()
        {
            return View();
        }
    }
}