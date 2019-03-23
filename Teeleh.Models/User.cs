using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using Teeleh.Models.Enums;
using Teeleh.Models.Helper;

namespace Teeleh.Models
{
    public class User
    {
        
        //Properties

        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }


        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string PSNId { get; set; }

        public string XBOXLive { get; set; }

        public Image UserImage { get; set; }

        public virtual List<Advertisement> Advertisements { get; set; }

        public List<Request> Requests { get; set; }

        public int ForgetPassCode { get; set; }

        public SessionState State { get; set; }

        public virtual List<AdBookmark> SavedAdvertisements { get; set; }

        public bool IsDeleted { get; set; }

        public List<Notification> Notifications { get; set; }

        [ForeignKey("User_Id")]
        public virtual List<Session> Sessions { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


        //Methods

        public IQueryable<object> GetAdBookmark(AppDbContext db)
        {
            var advertisements = db.AdBookmarks
                .Where(QueryHelper.GetAdBookmarksValidationQuery(Id))
                .Select(a=>a.Advertisement)
                .Where(QueryHelper.GetAdvertisementValidationQuery())
                .Select(QueryHelper.GetAdvertisementQuery());
            
            return advertisements;
        }

        public void CreateAdBookmark(AppDbContext db, int advertisementId)
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
    }

    
}
