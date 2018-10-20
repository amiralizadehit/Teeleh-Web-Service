using System.Collections.Generic;

namespace Teeleh.Models
{
    public class Platform
    {
        public static string PSN ="PSN";
        public static string XBOX = "XBOX";

        public string Id { get; set; }
        public string Name { get; set; }

        public List<Game> Games { get; set; }
    }
}
