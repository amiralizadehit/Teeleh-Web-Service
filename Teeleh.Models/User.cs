using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Models.Helper;
using Teeleh.Utilities;

namespace Teeleh.Models
{
    public class User
    {
        //Properties

        public int Id { get; set; }

        [Required] [StringLength(30)] public string FirstName { get; set; }

        [Required] [StringLength(30)] public string LastName { get; set; }

        [StringLength(11)] public string PhoneNumber { get; set; }

        [StringLength(11)] public string TemporaryPhoneNumber { get; set; }

        [Required] public string Password { get; set; }

        [EmailAddress] public string Email { get; set; }

        [EmailAddress] public string TemporaryEmail { get; set; }

        public string PSNId { get; set; }

        public string XBOXLive { get; set; }

        public virtual Location userProvince { get; set; }

        public int? userProvinceId { get; set; }

        public virtual Location userCity { get; set; }

        public int? userCityId { get; set; }

        public Image UserImage { get; set; }

        public virtual List<Advertisement> Advertisements { get; set; }

        public List<Request> Requests { get; set; }

        public int? SecurityToken { get; set; }

        public UserState State { get; set; }

        public virtual List<AdBookmark> SavedAdvertisements { get; set; }

        public List<Notification> Notifications { get; set; }

        [ForeignKey("User_Id")] public virtual List<Session> Sessions { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        //Methods

        
        ////////////////////////////// BOOKMARKS //////////////////////////////////////
        /// <summary>
        /// Returns all advertisement bookmarks of the user object it's called upon
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IQueryable<object> GetAdBookmark(AppDbContext db)
        {
            var advertisements = db.AdBookmarks
                .Where(QueryHelper.GetAdBookmarksValidationQuery(Id))
                .Select(a => a.Advertisement)
                .Where(QueryHelper.GetAdvertisementValidationQuery())
                .Select(QueryHelper.GetAdvertisementQuery());

            return advertisements;
        }


        /// <summary>
        /// Create an advertisement bookmark for the user it's called upon
        /// </summary>
        /// <param name="db"></param>
        /// <param name="advertisementId"></param>
        public void CreateAdBookmark(AppDbContext db, int advertisementId)
        {
            var bookmarkInDb = db.AdBookmarks.SingleOrDefault(b => b.AdvertisementId == advertisementId
                                                                   && b.UserId == Id);
            if (bookmarkInDb == null)
            {
                var bookmark = new AdBookmark()
                {
                    AdvertisementId = advertisementId,
                    UserId = Id,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                };
                db.AdBookmarks.Add(bookmark);
            }
            else
            {
                bookmarkInDb.IsDeleted = false;
            }
        }

        /// <summary>
        /// Delete the advertisement bookmark with specified Id of the user it's called upon
        /// </summary>
        /// <param name="db"></param>
        /// <param name="advertisementId"></param>
        public void DeleteAdBookmark(AppDbContext db, int advertisementId)
        {
            var bookmarkInDb = db.AdBookmarks.SingleOrDefault(a => a.UserId == Id
                                                                   && a.AdvertisementId == advertisementId);
            if (bookmarkInDb != null)
                bookmarkInDb.IsDeleted = true;
        }


        ////////////////////////////// NOTIFICATIONS //////////////////////////////////

        /// <summary>
        /// Returns all notifications of the user object it's called upon
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IQueryable<object> GetNotifications(AppDbContext db)
        {
            var notifications = db.Notifications.Where(n => n.UserId == Id)
                .Where(QueryHelper.GetNotificationsValidationQuery())
                .GroupBy(n => n.CreatedAt.Day)
                .OrderBy(g=>g.Key)
                .Select(QueryHelper.GetNotificationGroupByDate()); 
            return notifications;
        }

        /// <summary>
        /// Delete the notification with specified Id of the user it's called upon
        /// </summary>
        /// <param name="db"></param>
        /// <param name="notificationId"></param>
        public void DeleteNotification(AppDbContext db, int notificationId)
        {
            var notificationInDb = db.Notifications.SingleOrDefault(a => a.UserId == Id
                                                                   && a.Id == notificationId);
            if (notificationInDb != null)
                notificationInDb.Status = NotificationStatus.DELETED;
        }

        /// <summary>
        /// Mark the notification with specified Id as SEEN
        /// </summary>
        /// <param name="db"></param>
        /// <param name="notificationId"></param>
        public void MarkAsSeenNotifications(AppDbContext db, int notificationId)
        {
            var notificationInDb = db.Notifications.SingleOrDefault(a => a.UserId == Id
                                                                         && a.Id == notificationId);
            if (notificationInDb != null)
                notificationInDb.Status = NotificationStatus.SEEN;
        }



        


        /// <summary>
        /// Set temporary phone number and send security token to the user's new phone number.
        /// </summary>
        /// <param name="newPhoneNumber"></param>
        /// <returns>smsSent</returns>
        public bool ChangePhoneNumber(string newPhoneNumber)
        {
            bool smsSent = true;
            TemporaryPhoneNumber = newPhoneNumber;
            SecurityToken = RandomHelper.RandomInt(10000, 99999);
            if (MessageHelper.SendSMS_K(SecurityToken.ToString(), newPhoneNumber,
                    MessageHelper.SMSMode.VERIFICATION) != null)
            {
                smsSent = false;
            }
            return smsSent;
        }


        /// <summary>
        /// Checks whether token is equal to security token, if yes set the primary phone number with temporary.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        /// True : if old phone number has replaced with new one.
        /// False : if old phone number has not replaced with new one.
        /// </returns>
        public bool ValidateNewPhoneNumber(int token)
        {
            if (SecurityToken == token)
            {
                PhoneNumber = TemporaryPhoneNumber;
                return true;
            }
                return false;
        }


        /// <summary>
        /// Set user's information.
        /// </summary>
        public void SetUserInformation(UserInfoDto userInfo)
        {
            this.FirstName = userInfo.FirstName;
            this.LastName = userInfo.LastName;

            
            this.userProvinceId = userInfo.ProvinceId;
            this.userCityId = userInfo.CityId;

            this.XBOXLive = userInfo.XBOXLive;
            this.PSNId = userInfo.PSNId;
        }

        /// <summary>
        /// Set temporary email and send security token to the user's new mail address.
        /// </summary>
        /// <param name="newEmail"></param>
        public void ChangeEmail(string newEmail)
        {
            TemporaryEmail = newEmail;
            SecurityToken = RandomHelper.RandomInt(10000, 99999);
            MessageHelper.CodeVerificationEmail(SecurityToken.ToString(),newEmail,MessageHelper.EmailMode.VERIFICATION);
        }

        /// <summary>
        /// Checks whether token is equal to security token, if yes set the primary email with temporary.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        /// True : if old Email has been replaced with new one.
        /// False : if old Email has not been replaced with new one.
        /// </returns>
        public bool ValidateNewEmail(int token)
        {
            if (SecurityToken == token)
            {
                Email = TemporaryEmail;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Change Password of a user
        /// </summary>
        /// <param name="newPassword"></param>
        public void ChangePassword(string newPassword)
        {
            var hashed = Utilities.HasherHelper.sha256_hash(newPassword);
            this.Password = hashed;
        }

    }
}