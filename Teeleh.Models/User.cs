using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public Image UserImage { get; set; }

        public virtual List<Advertisement> Advertisements { get; set; }

        public List<Request> Requests { get; set; }

        public int? SecurityToken { get; set; }

        public State State { get; set; }

        public virtual List<AdBookmark> SavedAdvertisements { get; set; }

        public bool IsDeleted { get; set; }

        public List<Notification> Notifications { get; set; }

        [ForeignKey("User_Id")] public virtual List<Session> Sessions { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


        //Methods


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


        /// <summary>
        /// Set temporary phone number and send security token to the user it's called upon via Email and SMS (if possible)
        /// </summary>
        /// <param name="newPhoneNumber"></param>
        /// <returns>(mailSent, smsSent)</returns>
        public Tuple<bool, bool> ChangePhoneNumber(string newPhoneNumber)
        {
            bool mailSent = true;
            bool smsSent = true;
            TemporaryPhoneNumber = newPhoneNumber;
            SecurityToken = RandomHelper.RandomInt(10000, 99999);

            if (PhoneNumber != null)
            {
                if (MessageHelper.SendSMS_K(SecurityToken.ToString(), PhoneNumber,
                        MessageHelper.SMSMode.VERIFICATION) != null)
                    smsSent = false;
            }
            else
            {
                smsSent = false;
            }
            if (Email != null)
            {
                MessageHelper.CodeVerificationEmail(SecurityToken.ToString(), PhoneNumber,
                    MessageHelper.EmailMode.VERIFICATION);
            }
            else
            {
                mailSent = false;
            }

            return Tuple.Create(mailSent,smsSent);
        }


        /// <summary>
        /// Checks whether token is equal to security token, is yes set the primary phone number with temporary.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool ValidateNewPhoneNumber(int token)
        {
            if (SecurityToken == token)
            {
                PhoneNumber = TemporaryPhoneNumber;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}