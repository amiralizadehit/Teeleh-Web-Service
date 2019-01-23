using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Teeleh.Utilities
{
    public class NotificationHelper
    {

        //Firebase Spec
        private static string ServerAPIKey =
            "AAAAoqnTIRs:APA91bERde3Q6GxZi8OXIUPeQlDGfCt3DiwAWVzatf7jS0IfE-9ZALfAGqsoTWnYw9eYPVlhnnb25uzz2m9K1QuplJC1wqvo5mVQIxNFjCH2Pp5EoZW62cju8uF2d0qph5CNSJeoxEKQ";

        private static string SenderID = "698633888027";


        public static Exception SendNotification(string token)
        {
            var data = new
            {
                to = token,
                data = new
                {
                    message = "Hi your test notification works",
                    title = "Teeleh"
                }
            };

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);

            try
            {

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add($"Authorization: key={ServerAPIKey}");
                tRequest.Headers.Add($"Sender: id={SenderID}");

                tRequest.ContentLength = byteArray.Length;
                Stream stream = tRequest.GetRequestStream();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Close();

                WebResponse response = tRequest.GetResponse();
                stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                string responseFromServer = reader.ReadToEnd();

                reader.Close();
                stream.Close();
                response.Close();

                return null;
            }

            catch (Exception ex)
            {
                return ex;
            }

        }
    }
}
