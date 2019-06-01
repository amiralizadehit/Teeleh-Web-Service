using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teeleh.Models;
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
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SendNotification(CasualNotificationDto casualNotification)
        {
            if (ModelState.IsValid)
            {
                string imageFilePath = "";
                var folderRandomIndex = RandomHelper.RandomInt(0, 10000);
                if (casualNotification.Avatar != null)
                {
                    var fileRandomIndex = RandomHelper.RandomInt(0, 10000);

                    var fileExtension = Path.GetExtension(casualNotification.Avatar.FileName);

                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/Image/Notification/" + folderRandomIndex));

                    var fileName = fileRandomIndex + DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") +
                                   fileExtension;
                    imageFilePath = "/Image/Notification/" + folderRandomIndex + "/" + fileName;
                    fileName =
                        Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Image/Notification/" + folderRandomIndex + "/"), fileName);
                    casualNotification.Avatar.SaveAs(fileName);
                }
                else
                {
                    //Default
                    imageFilePath = "/Image/Games/Default/Default.jpg";
                }

                Image image = new Image()
                {
                    CreatedAt = DateTime.Now,
                    Type = ImageType.NOTIFICATOIN_IMAGE,
                    ImagePath = imageFilePath,
                    Name = casualNotification.Title,
                    UpdatedAt = DateTime.Now
                };

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