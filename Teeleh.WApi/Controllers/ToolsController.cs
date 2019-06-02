using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teeleh.Models;
using Teeleh.Models.CustomValidation.Website;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Utilities;
using Teeleh.Utilities.Notification_Object;
using Teeleh.WApi.Functions;

namespace Teeleh.WApi.Controllers
{
    public class ToolsController : Controller
    {

        private AppDbContext db;

        public ToolsController()
        {
            db = new AppDbContext();
        }
        // GET: Tools
        
        [SessionTimeout]
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [SessionTimeout]
        public JsonResult SendNotification(CasualNotificationDto casualNotification)
        {
            if (ModelState.IsValid)
            {

                Image image;
                if (casualNotification.Avatar != null)
                {
                    image = ImageHandler.CreateWebImage(casualNotification.Avatar, casualNotification.Title,
                        WebImageType.NOTIFICATION_IMAGE);
                }
                else
                {
                    image = ImageHandler.CreateDefaultImage(casualNotification.Title, WebImageType.NOTIFICATION_IMAGE);
                }

                NotificationGenerator.CasualNotification(db, casualNotification.Title, casualNotification.Message, image);

                return Json(new
                {
                    result = true
                });
            }
            return Json(new
            {
                result = false
            });
        }
        
    }
}