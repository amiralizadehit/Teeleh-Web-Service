using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Teeleh.Models;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Utilities;
using Teeleh.WApi.Functions;

namespace Teeleh.WApi.Controllers.API
{
    public class NotificationsController : ApiController
    {
        private AppDbContext db;

        public NotificationsController()
        {
            db = new AppDbContext();
        }

        [HttpPost]
        [Route("api/notifications/send")]
        public IHttpActionResult Send(CasualNotificationDto casualNotification)
        {
            if (ModelState.IsValid)
            {
                string imageFilePath = "";
                var folderRandomIndex = RandomHelper.RandomInt(0, 10000);
                if (casualNotification.Avatar != null)
                {
                    var fileRandomIndex = RandomHelper.RandomInt(0, 10000);

                    var fileExtension = Path.GetExtension(casualNotification.Avatar.FileName);

                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Image/Notification/" + folderRandomIndex));

                    var fileName = fileRandomIndex + DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") +
                                   fileExtension;
                    imageFilePath = "/Image/Notification/" + folderRandomIndex + "/" + fileName;
                    fileName =
                        Path.Combine(HttpContext.Current.Server.MapPath("~/Image/Notification/" + folderRandomIndex + "/"), fileName);
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

                return Ok();
            }
            return BadRequest();
        }

    }
}