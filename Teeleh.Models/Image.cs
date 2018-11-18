using System;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;

namespace Teeleh.Models
{
    public class Image
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public ImageType Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public enum ImageType
        {
            AVATAR,
            COVER,
            USER_IMAGE
        }
    }
}
