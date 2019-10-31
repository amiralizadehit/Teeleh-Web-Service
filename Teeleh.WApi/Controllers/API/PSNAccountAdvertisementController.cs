using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Teeleh.Models;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Models.Helper;
using Teeleh.WApi.Functions;

namespace Teeleh.WApi.Controllers.API
{
    /// <summary>
    /// This class is used to manage client requests related to PSN Account Advertisements.
    /// </summary>
    public class PSNAccountAdvertisementController : ApiController
    {
        private AppDbContext db;

        public PSNAccountAdvertisementController()
        {
            db = new AppDbContext();
        }

        /// <summary>
        /// This endpoint creates a psn account advertisement with the given information.
        /// </summary>
        /// <returns>200 : Advertisement Created |
        /// 401 : Session info not found |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPost]
        [Route("api/advertisements_accounts/psn/create")]
        public async Task<IHttpActionResult> Create(PSNAccountAdvertisementCreateDto advertisementCreate)
        {
            if (ModelState.IsValid)
            {
                var session = await
                    db.Sessions.SingleOrDefaultAsync(
                        QueryHelper.GetSessionObjectValidationQuery(advertisementCreate.Session));
                if (session != null)
                {
                    var user = session.User;


                    var isImageEmpty = string.IsNullOrEmpty(advertisementCreate.UserImage);

                    Image userImage = null;
                    if (!isImageEmpty)
                    {
                        //we write user's uploaded image in memory
                        userImage = ImageHandler.CreateUserImage(db, user.Id, advertisementCreate.UserImage);
                    }
                    var games = db.Games.Where(p => advertisementCreate.Games.Contains(p.Id)).ToList();
                    var newAdvertisement = new PSNAccountAdvertisement()
                    {
                        Games = games,
                        User = user,
                        Capacity = advertisementCreate.Capacity,
                        Region = advertisementCreate.Region,
                        Type = advertisementCreate.Type,
                        Price = advertisementCreate.Price,
                        Caption = advertisementCreate.Caption,
                        HasPlus = advertisementCreate.HasPlus,
                        UserImage = userImage,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    db.PsnAccountAdvertisements.Add(newAdvertisement);

                    await db.SaveChangesAsync();

                    // Broadcasting

                    //NotificationGenerator.NewAdvertisementNotification(db, newAdvertisement);

                    return Ok(newAdvertisement.Id);
                }

                return Unauthorized();
            }

            return BadRequest();
        }
    }
}