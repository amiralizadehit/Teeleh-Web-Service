using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
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
        private string localDomain;

        public AdvertisementsController()
        {
            db = new AppDbContext();
            localDomain = "http://" + HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// This endpoint returns a list of games.
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
                    Id = a.Id,
                    Game = a.Game.Name,
                    Avatar = localDomain + a.Game.Avatar.ImagePath,
                    UserImage = localDomain + a.UserImage.ImagePath,
                    Platform = a.Platform.Name,
                    MedType = a.MedType,
                    Caption = a.Caption,
                    GameRegion = a.GameReg,
                    Location = new
                    {
                        Province = a.LocationProvince.Name,
                        City = a.LocationCity.Name,
                        Region = a.LocationRegion.Name,
                        Latitude = a.Latitude,
                        Longitude = a.Longitude
                    },
                    CreatedAt = a.CreatedAt,
                    Price = a.Price,
                    ExchangeGames = a.ExchangeGames.Select(g => new
                    {
                        Name = g.Game.Name,
                        Avatar = localDomain + g.Game.Avatar.ImagePath
                    })
                }).ToList();

            return Ok(advertisements);
        }


        [HttpPost]
        [Route("api/advertisements")]
        public IHttpActionResult GetAdvertisements(Filter filter)
        {
            if (ModelState.IsValid)
            {
                var pageSize = 10;
                var platforms = filter.PlatformIds;
                var minPrice = (filter.MinPrice ?? 0);
                var maxPrice = (filter.MaxPrice ?? 3000000);
                var provinceId = filter.LocationProvinceId;
                var cityId = filter.LocationCityId;
                var mediaType = filter.MedType;
                var pageNumber = (filter.PageNumber ?? 1);
                var sort = (filter.Sort ?? Sort.NEWEST);

                var query = db.Advertisements.Where(a => a.Price >= minPrice && a.Price <= maxPrice)
                    .Where(d => d.isDeleted == false);


                if (provinceId != null)
                {
                    query = query.Where(f => f.LocationProvinceId == provinceId);
                }

                if (cityId != null)
                {
                    query = query.Where(c => c.LocationCityId == cityId);
                }

                if (mediaType != null)
                {
                    query = query.Where(m => m.MedType == mediaType);
                }

                if (platforms != null)
                {
                    query = query.Where(p => platforms.Any(x => x == p.PlatformId));
                }

                if (sort == Sort.NEWEST)
                    query = query.OrderByDescending(t => t.CreatedAt);
                else if (sort == Sort.PRICE_ASCENDING)
                    query = query.OrderBy(t => t.Price);
                else
                    query = query.OrderByDescending(t => t.Price);

                var advertisements = query.Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .Select(a => new
                    {
                        Id = a.Id,
                        Game = a.Game.Name,
                        Avatar = localDomain + a.Game.Avatar.ImagePath,
                        Platform = a.Platform.Name,
                        MedType = a.MedType,
                        Caption = a.Caption,
                        UserImage = localDomain + a.UserImage.ImagePath,
                        GameRegion = a.GameReg,
                        Location = new
                        {
                            Province = a.LocationProvince.Name,
                            City = a.LocationCity.Name,
                            Region = a.LocationRegion.Name,
                            Latitude = a.Latitude,
                            Longitude = a.Longitude
                        },
                        Age = SqlFunctions.DateDiff("minute", a.CreatedAt, DateTime.Now),
                        Price = a.Price,
                        ExchangeGames = a.ExchangeGames.Select(g => new
                        {
                            Name = g.Game.Name,
                            Avatar = localDomain + g.Game.Avatar.ImagePath
                        })
                    }).ToList();
                return Ok(advertisements);
            }

            return BadRequest();
        }


        /// <summary>
        /// This endpoint returns a list of games.
        /// </summary>
        /// <returns>200 : sent 
        /// </returns>
        /*[HttpPost]
        [Route("api/advertisements/feed/")]
        public IHttpActionResult GetAdvertisements(FilterConfig )
        {

        }*/

        /// <summary>
        /// This endpoint creates an advertisementCreate with given information.
        /// </summary>
        /// <returns>200 : Advertisement Created |
        /// 401 : Session info not found |
        /// 400 : Bad Request 
        /// </returns>
        [HttpPost]
        [Route("api/advertisements/create")]
        public async Task<IHttpActionResult> Create(AdvertisementCreateDto advertisementCreate)
        {
            if (ModelState.IsValid)
            {
                var session = await
                    db.Sessions.SingleOrDefaultAsync(s => s.SessionKey == advertisementCreate.SessionInfo.SessionKey &&
                                                          s.Id == advertisementCreate.SessionInfo.SessionId &&
                                                          s.State == SessionState.Active);
                if (session != null)
                {
                    var user = session.User;
                    Models.Image imageInDb = null;

                    if (!string.IsNullOrEmpty(advertisementCreate.UserImage))
                    {
                        Image image;
                        var byteArray = Convert.FromBase64String(advertisementCreate.UserImage);
                        Directory.CreateDirectory(
                            HttpContext.Current.Server.MapPath("~/Image/Advertisements/" + user.Id));
                        string folderPath =
                            HttpContext.Current.Server.MapPath("~/Image/Advertisements/" + user.Id + "/");
                        string fileName = "UserImage" + "_" + DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") + ".jpg";
                        string imagePath = folderPath + fileName;
                        string dbPath = "/Image/Advertisements/" + user.Id + "/" + fileName;
                        using (MemoryStream mStream = new MemoryStream(byteArray))
                        {
                            image = Image.FromStream(mStream);
                            image.Save(imagePath, ImageFormat.Jpeg);
                        }

                        imageInDb = new Models.Image()
                        {
                            Name = "User" + "_" + session.User.Id + "Ad",
                            ImagePath = dbPath,
                            Type = Models.Image.ImageType.USER_IMAGE,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };
                        db.Images.Add(imageInDb);
                        await db.SaveChangesAsync();
                    }

                    var new_advertisement = new Advertisement()
                    {
                        User = user,
                        MedType = (Advertisement.MediaType) advertisementCreate.MedType,
                        GameId = advertisementCreate.GameId,
                        GameReg = (Advertisement.GameRegion) advertisementCreate.GameReg,
                        Latitude = advertisementCreate.Latitude,
                        Longitude = advertisementCreate.Longitude,
                        LocationRegionId = advertisementCreate.LocationRegionId,
                        LocationCityId = advertisementCreate.LocationCityId,
                        LocationProvinceId = advertisementCreate.LocationProvinceId,
                        Price = advertisementCreate.Price,
                        PlatformId = advertisementCreate.PlatformId,
                        Caption = advertisementCreate.Caption,
                        UserImage = imageInDb,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    db.Advertisements.Add(new_advertisement);

                    await db.SaveChangesAsync();

                    if (advertisementCreate.ExchangeGames.Count != 0) //we have some games to exchange
                    {
                        foreach (var game in advertisementCreate.ExchangeGames)
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
                    return Ok(new_advertisement.Id);
                }

                return Unauthorized();
            }

            return BadRequest();
        }

        /// <summary>
        /// This endpoint cancels an advertisementCreate with given id.
        /// </summary>
        /// <returns>200 : Ok |
        /// 401 : Session info not found |
        /// 400 : Bad Request |
        /// 404 : Advertisement not found
        /// </returns>
        [HttpPost]
        [Route("api/advertisements/cancel")]
        public async Task<IHttpActionResult> Delete(IDPairDto pair)
        {
            if (ModelState.IsValid)
            {
                var sessionInDb = await
                    db.Sessions.SingleOrDefaultAsync(s => s.SessionKey == pair.session.SessionKey &&
                                                          s.Id == pair.session.SessionId &&
                                                          s.State == SessionState.Active);
                if (sessionInDb != null)
                {
                    var advertisementInDb = db.Advertisements.SingleOrDefault(a => a.Id == pair.Id);
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
        /// Returns an advertisementCreate in a more detailed manner with given id.
        /// </summary>
        /// <returns>200 : Ok |s
        /// 404 : Advertisement Not Found |
        /// 400 : Bad Request
        /// </returns>
        /*[Route("api/advertisements/detail/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Detail(int id)
        {
            var advertisementInDb = db.Advertisements.Where(a => a.Id == id && a.isDeleted == false).
                Select(f=>new
                {
                    UserImage = f.UserImage.ImagePath,
                    Caption = f.Caption,
                    Map = new
                    {
                        Latitude = f.Latitude,
                        Longitude = f.Longitude,
                    },
                    Adtype = f.MedType,
                    Location = f.LocationRegion,
                    Platform = f.Platform.FirstName,
                    Similar = db.Advertisements.Where(a=>a.GameId==f.GameId)

                });

            return NotFound();
        }*/
    }
}