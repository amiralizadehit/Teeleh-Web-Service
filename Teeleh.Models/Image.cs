using System;

namespace Teeleh.Models
{
    public class Image
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string ImagePath {
            get {return UrlContent(ImagePath);}
            set { ImagePath = value; }
        }
        public ImageType Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public enum ImageType
        {
            AVATAR,
            COVER,
            USER_IMAGE
        }

        public string UrlContent(string imagePath)
        {
            return UrlContent(imagePath);
        }
    }
}
