using System.Collections.Generic;

namespace Teeleh.Models
{
    public class Platform
    {
        public static string PS4 ="PS4";
        public static string XBOX = "XBOX ONE";
        public static string PC = "PC";
        public static string Switch = "Switch";
        public static string Android = "Android";
        public static string iOs = "iOS";

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual List<Game> Games { get; set; }
    }
}
