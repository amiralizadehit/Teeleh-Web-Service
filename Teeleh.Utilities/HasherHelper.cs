using System.Security.Cryptography;
using System.Text;

namespace Teeleh.Utilities
{
    public class HasherHelper
    {

        public static string sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                {
                    Sb.Append(b.ToString("X2"));
                }

                return Sb.ToString();
            }
        }
    }
}
