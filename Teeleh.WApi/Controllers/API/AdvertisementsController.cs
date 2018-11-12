using System;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Teeleh.Models;
using Teeleh.Models.Dtos;
using Teeleh.Models.ViewModels;
using Image = System.Drawing.Image;

namespace Teeleh.WApi.Controllers
{
    /// <summary>
    /// This class is used to manage client requests related to advertisements.
    /// </summary>
    public class AdvertisementsController : ApiController
    {
        private AppDbContext db;

        public AdvertisementsController()
        {
            db = new AppDbContext();
        }

        /// <summary>
        /// This endpoint returns a list of games which contains their id, name and avatar photo.
        /// </summary>
        /// <returns>200 : sent 
        /// </returns>
        [HttpGet]
        [Route("api/advertisements")]
        public IHttpActionResult GetAdvertisements()
        {
            var advertisements = db.Advertisements.Where(g => g.isDeleted == false)
                .Select(a => new
                {
                    Game = a.Game.Name,
                    Avatar = Url.Content(a.Game.Avatar.ImagePath),
                    Platform = a.Platform.Name,
                    Adtype = a.AdType,
                    CreatedAt = a.CreatedAt,
                    Price = a.Price
                }).ToList();

            return Ok(advertisements);
        }

        /// <summary>
        /// This endpoint creates an advertisement with given information.
        /// </summary>
        /// <returns>200 : Advertisement Created |
        /// 401 : Session info not found |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPost]
        [Route("api/advertisements/create")]
        public async Task<IHttpActionResult> Create(AdvertisementDto advertisement)
        {
            if (ModelState.IsValid)
            {
                var session = await
                    db.Sessions.SingleOrDefaultAsync(s => s.SessionKey == advertisement.SessionInfo.SessionKey);
                if (session != null)
                {
                    var user = session.User;
                    Image image;

                    var byteArray = System.Convert.FromBase64String(advertisement.UserImage);
                    using (MemoryStream mStream = new MemoryStream(byteArray))
                    {
                        image = Image.FromStream(mStream);
                    }

                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Image/Advertisements/" + user.Id));
                    string folderPath = HttpContext.Current.Server.MapPath("~/Image/Advertisements/" + user.Id);
                    string fileName = "UserImage" + "_" + DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") + ".jpg";
                    string imagePath = folderPath + fileName;
                    string dbPath = "~/Image/Advertisements/" + session.User.Id + fileName;
                    image.Save(imagePath, ImageFormat.Jpeg);

                    var imageInDb = new Teeleh.Models.Image()
                    {
                        Name = "User" + "_" + session.User.Id + "Ad",
                        ImagePath = dbPath,
                        Type = Models.Image.ImageType.USER_IMAGE,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    db.Images.Add(imageInDb);
                    await db.SaveChangesAsync();



                    var new_advertisement = new Advertisement()
                    {
                        User = user,
                        AdType = (Advertisement.AdvertisementType) advertisement.AdType,
                        GameId = advertisement.GameId,
                        Latitude = advertisement.Latitude,
                        Longitude = advertisement.Longitude,
                        LocationId = advertisement.LocationId,
                        Price = advertisement.Price,
                        PlatformId = advertisement.PlatformId,
                        Caption = advertisement.Caption,
                        UserImage = imageInDb,
                    };
                    db.Advertisements.Add(new_advertisement);

                    await db.SaveChangesAsync();

                    if (advertisement.ExchangeGames.Count != 0) //we have some games to exchange
                    {
                        foreach (var game in advertisement.ExchangeGames)
                        {
                            var newExchange = new Exchange()
                            {
                                AdvertisementId = new_advertisement.Id,
                                GameId = game
                            };
                            db.Exchanges.Add(newExchange);
                        }
                    }

                    await db.SaveChangesAsync();
                }

                return Unauthorized();
            }

            return BadRequest();
        }

        /// <summary>
        /// This endpoint cancels an advertisement with given id.
        /// </summary>
        /// <returns>200 : Ok |
        /// 401 : Session info not found |
        /// 400 : Bad Request |
        /// 404 : Advertisement not found
        /// </returns>
        [HttpPost]
        [Route("api/advertisements/cancel/{id}")]
        public async Task<IHttpActionResult> Delete(SessionInfoObject session, int id)
        {
            if (ModelState.IsValid)
            {
                var sessionInDb = await db.Sessions.SingleOrDefaultAsync(s => s.SessionKey == session.SessionKey);
                if (sessionInDb != null)
                {
                    var advertisementInDb = db.Advertisements.SingleOrDefault(a => a.Id == id);
                    if (advertisementInDb != null)
                    {
                        advertisementInDb.isDeleted = true;
                        await db.SaveChangesAsync();
                        return Ok();
                    }

                    return NotFound();
                }

                return Unauthorized();
            }

            return BadRequest();
        }

        /// <summary>
        /// Returns an advertisement in a more detailed manner with given id.
        /// </summary>
        /// <returns>200 : Ok |
        /// 404 : Advertisement Not Found |
        /// 400 : Bad Request
        /// </returns>
        [Route("api/advertisements/detail/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Detail(int id)
        {
            var advertise = await db.Advertisements.Include(g => g.Game)
                .Include(l => l.Location)
                .Include(p => p.Platform)
                .Include(i => i.UserImage)
                .Include(e => e.ExchangeGames)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (advertise != null)
            {
                return Ok(advertise);
            }

            return NotFound();
        }
    }
}