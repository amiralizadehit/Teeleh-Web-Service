using System.Collections.Generic;

namespace Teeleh.Models
{
    public class Platform
    {
        public static string PS4 ="PS4";
        public static string PS3 ="PS3";
        public static string XBOX = "XBOX";
        public static string XBOX360 = "XBOX360";
        public static string PC = "PC";
        public static string Switch = "Switch";
        public static string Android = "Android";
        public static string iOS = "iOS";

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual List<Game> Games { get; set; }
        public List<Request> Requests { get; set; }
    }
}
