using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using Teeleh.Models;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Utilities;
using Teeleh.WApi.Controllers;
using Image = System.Drawing.Image;

namespace Teeleh.WApi.Functions
{
    public class ImageHandler
    {
        /// <summary>
        /// This helper function store uploaded images in memory
        /// </summary>
        public static Models.Image CreateUserImage(AppDbContext db, int Id, string imageToStore)
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

        public static Models.Image CreateWebImage(HttpPostedFileBase file, string name, WebImageType type)

        {
            string baseAddressInMemory = "";
            ImageType imageType;
            switch (type)
            {
                case WebImageType.AVATAR:
                    baseAddressInMemory = "/Image/Games/";
                    imageType = ImageType.AVATAR;
                    break;
                case WebImageType.COVER:
                    baseAddressInMemory = "/Image/Games/";
                    imageType = ImageType.COVER;
                    break;
                case WebImageType.GAMEPLAY:
                    baseAddressInMemory = "/Image/Games/";
                    imageType = ImageType.GAMEPLAY;
                    break;
                case WebImageType.NOTIFICATION_IMAGE:
                    baseAddressInMemory = "/Image/Notifications/";
                    imageType = ImageType.NOTIFICATION_IMAGE;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }


            string avatarPhotoFilePath = "";
            var folderRandomIndex = RandomHelper.RandomInt(0, 10000);
            var fileRandomIndex = RandomHelper.RandomInt(0, 10000);

            var avatarPhotoFileExtension = Path.GetExtension(file.FileName);

            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~" + baseAddressInMemory + folderRandomIndex));

            var avatarPhotoFileName = fileRandomIndex + DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") +
                                      avatarPhotoFileExtension;
            avatarPhotoFilePath = baseAddressInMemory + folderRandomIndex + "/" + avatarPhotoFileName;
            avatarPhotoFileName =
                Path.Combine(HttpContext.Current.Server.MapPath("~"+baseAddressInMemory + folderRandomIndex + "/"), avatarPhotoFileName);
            file.SaveAs(avatarPhotoFileName);

            //We create image instance and store it in database
            var webImage = new Models.Image()
            {
                Name = name,
                ImagePath = avatarPhotoFilePath,
                Type = imageType,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
      
            return webImage;
        }

        public static Models.Image CreateDefaultImage(string name, WebImageType type)
        {
            string defaultPath = "";
            ImageType imageType;
            switch (type)
            {   
                case WebImageType.AVATAR:
                    defaultPath = "/Image/Games/Default/Default.jpg";
                    imageType = ImageType.AVATAR;
                    break;
                case WebImageType.COVER:
                   defaultPath = "/Image/Games/Default/DefaultCover.jpg";
                    imageType = ImageType.COVER;
                    break;
                case WebImageType.NOTIFICATION_IMAGE:
                    defaultPath = "/Image/Games/Default/Default.jpg";
                    imageType = ImageType.NOTIFICATION_IMAGE;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            //We create image instance and store it in database
            var defaultImage = new Models.Image()
            {
                Name = name,
                ImagePath = defaultPath,
                Type = imageType,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            return defaultImage;
        }

    }
}