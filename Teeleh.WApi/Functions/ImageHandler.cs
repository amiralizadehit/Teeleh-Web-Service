using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using Teeleh.Models;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Image = System.Drawing.Image;

namespace Teeleh.WApi.Functions
{
    public class ImageHandler
    {

        /// <summary>
        /// This helper function store uploaded images in memory
        /// </summary>
        public static Models.Image StoreImage(AppDbContext db, int Id, string imageToStore)
        {
            Image image;
            string dbPath = "";
            var byteArray = Convert.FromBase64String(imageToStore);
            Directory.CreateDirectory(
                HttpContext.Current.Server.MapPath("~/Image/Advertisements/" + Id));
            string folderPath =
                HttpContext.Current.Server.MapPath("~/Image/Advertisements/" + Id + "/");
            string fileName = "UserImage" + "_" + DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") + ".jpg";
            string imagePath = folderPath + fileName;
            dbPath = "/Image/Advertisements/" + Id + "/" + fileName;
            using (MemoryStream mStream = new MemoryStream(byteArray))
            {
                image = Image.FromStream(mStream);
                image.Save(imagePath, ImageFormat.Jpeg);
            }

            //We create image instance to store it in db with associated advertisement
            var userImage = new Models.Image()
            {
                Name = "User" + "_" + Id + "Ad",
                ImagePath = dbPath,
                Type = ImageType.USER_IMAGE,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            db.Images.Add(userImage);
            return userImage;
        }

    }
}