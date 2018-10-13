using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teeleh.Utilities
{
    public class RandomHelper
    {
        private static Random random = new Random();
        public static int RandomInt(int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            return random.Next(minValue, maxValue);
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "abcdefghijklmnopqrstuvwxyz" + "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
