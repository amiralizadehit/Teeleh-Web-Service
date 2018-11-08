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
using Image = System.Drawing.Image;

namespace Teeleh.WApi.Controllers
{
    public class AdvertisementsController : ApiController
    {
        private AppDbContext db;

        public AdvertisementsController()
        {
            db = new AppDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetAdvertisements()
        {
            var advertisements = db.Advertisements
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

        [HttpPost]
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
                    using (MemoryStream mStream = new MemoryStream(advertisement.UserImage))
                    {
                         image = Image.FromStream(mStream);
                    }
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Image/Advertisements/"+user.Id));
                    string folderPath = HttpContext.Current.Server.MapPath("~/Image/Advertisements/" + user.Id);
                    string fileName = "UserImage" + "_" + DateTime.Now.ToString("yy-MM-dd-hh-mm-ss")+".jpg"; 
                    string imagePath = folderPath+fileName;
                    string dbPath = "~/Image/Advertisements/" + session.User.Id + fileName;
                    image.Save(imagePath,ImageFormat.Jpeg);

                    var imageInDb = new Teeleh.Models.Image()
                    {
                        Name = "User"+"_"+session.User.Id+"Ad",
                        ImagePath = dbPath,
                        Type = Models.Image.ImageType.USER_IMAGE,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    db.Images.Add(imageInDb);
                    await db.SaveChangesAsync();

                    var imageId = imageInDb.Id;
                    

                    
                    var games_to_exchange = db.Games.Where(g => advertisement.GamesToExchange.Contains(g.Id)).ToList();

                    var new_advertisement = new Advertisement()
                    {
                        User = user,
                        AdType = (Advertisement.AdvertisementType)advertisement.AdType,
                        GameId = advertisement.GameId,
                        Latitude = advertisement.Latitude,
                        Longitude = advertisement.Longitude,
                        LocationId = advertisement.LocationId,
                        Price = advertisement.Price,
                        PlatformId = advertisement.PlatformId,
                        caption = advertisement.caption,
                        UserImageId = imageId,
                        GamesToExchange = games_to_exchange
                        
                    };
                    db.Advertisements.Add(new_advertisement);
                    await db.SaveChangesAsync();
                }

                return Unauthorized();
            }

            return BadRequest();
        }


        /// <summary>
        /// Returns an advertisement in a more detailed manner with given id
        /// </summary>
        /// <returns>200 : Ok |
        /// 404 : Advertisement Not Found |
        /// 400 : Bad Request
        /// </returns>
        [Route("api/advertisements/detail/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Detail(int id)
        {
            var advertise = await db.Advertisements.Include(g=>g.Game)
                .Include(l=>l.Location)
                .Include(p=>p.Platform)
                .Include(i=>i.UserImage)
                .Include(e=>e.GamesToExchange)
                .SingleOrDefaultAsync(a=>a.Id==id);

            if (advertise != null)
            {
                return Ok(advertise);
            }

            return NotFound();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

    }
}