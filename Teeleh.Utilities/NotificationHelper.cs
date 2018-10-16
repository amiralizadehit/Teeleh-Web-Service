﻿using Kavenegar;
using System;
using System.Net.Mail;

namespace Teeleh.Utilities
{
    public class NotificationHelper
    {
        //Email
        private static string ZohoMailAddress= "noreply@teelehdev.ir";
        private static string ZohoMailPassword= "teeleh2018Abc";
        private static string ZohoMailHost= "smtp.zoho.com";

        //Kavenegar Spec
        private static string KavenegarApiKey = "5058466E65505161535A63627539584B592F753769773D3D";
        private static string KavenegarSenderNumber = "10000066600600 ";

        //Twilio Spec
        private static string TwilioAccountSid = "AC52512b29616508da13ce38f06646e2dc";
        private static string authToken = "de66d3f1f115e706bafa4cada80a863a";

        //Kavenegar
        public static Exception CodeVerificationSMS_K(string token, string reciever)
        {
            try
            {
                var api = new KavenegarApi(KavenegarApiKey);
                api.VerifyLookup(reciever, token, "TeelehVerification");
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static Exception ForgetPasswordSMS_K(string token, string reciever)
        {
            try
            {
                var api = new KavenegarApi(KavenegarApiKey);
                api.VerifyLookup(reciever, token, "TeelehForgetPassword");
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static void CodeVerificationEmail(string token, string receiver)
        {
            string body = "Your Verification Code is : " + token;
            SmtpClient smtpClient = new SmtpClient(ZohoMailHost);
            var mail = new MailMessage();
            mail.From = new MailAddress(ZohoMailAddress);
            mail.To.Add(receiver);
            mail.Subject = "Teeleh Verification Code";
            mail.IsBodyHtml = true;
            mail.Body = body;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(ZohoMailAddress, ZohoMailPassword);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(mail);
        }






        /*public static bool CodeVerificationSMS_T(string token, string receiver)
        {
            TwilioClient.Init(TwilioAccountSid, authToken);

            var message = MessageResource.Create(
                body: "Your Teeleh verification code is :" + Environment.NewLine + token,
                from: new Twilio.Types.PhoneNumber("+15017122661"),
                to: new Twilio.Types.PhoneNumber(receiver)
            );
        }*/
    }
}