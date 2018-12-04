using System;
using System.CodeDom;
using System.Collections.Generic;
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
                    Game = a.Game.Name,
                    Avatar = a.Game.Avatar.ImagePath,
                    Platform = a.Platform.Name,
                    MedType = a.MedType,
                    Caption = a.Caption,
                    GameRegion = a.GameReg,
                    Location = new
                    {
                        Province = a.LocationProvince.Name,
                        City = a.LocationCity.Name,
                        Region = a.LocationRegion.Name
                    },
                    CreatedAt = a.CreatedAt,
                    Price = a.Price,
                    ExchangeGames = a.ExchangeGames.Select(g=>new
                    {
                        g.Game.Name,
                        g.Game.Avatar
                    })

                }).AsEnumerable().Select(v=>new
                {
                    Game = v.Game,
                    Avatar = Url.Content(v.Avatar),
                    Caption = v.Caption,
                    MedType = v.MedType,
                    GameRegion = v.GameRegion,
                    Location = v.Location,
                    Platform = v.Platform,
                    Price = v.Price,
                    CreateAt = v.CreatedAt,
                    ExchangeGames = v.ExchangeGames.Select(g=>new
                    {
                        Name = g.Name,
                        Avatar = Url.Content(g.Avatar.ImagePath)
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
                var ProvinceId = filter.LocationProvinceId;
                var CityId = filter.LocationCityId;
                var mediaType = filter.MedType;
                var pageNumber = (filter.PageNumber ?? 1);

                var final = db.Advertisements.Where(a => (a.Price > minPrice && a.Price < maxPrice && a.isDeleted==false));

                if (ProvinceId!=null)
                {
                    final = final.Where(f => f.LocationProvinceId == ProvinceId);
                }

                if (CityId != null)
                {
                    final = final.Where(c => c.LocationCityId == CityId);
                }

                if (mediaType!=null)
                {
                    final = final.Where(m => (int)m.MedType == mediaType);
                }

                if (platforms != null)
                {
                    final = final.Where(p => platforms.Contains(p.PlatformId));
                }
                    final.Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .OrderByDescending(t=>t.CreatedAt)
                    .Select(a => new
                    {
                        Game = a.Game.Name,
                        Avatar = a.Game.Avatar.ImagePath,
                        Platform = a.Platform.Name,
                        MedType = a.MedType,
                        Caption = a.Caption,
                        GameRegion = a.GameReg,
                        Location = new
                        {
                            Province = a.LocationProvince.Name,
                            City = a.LocationCity.Name,
                            Region = a.LocationRegion.Name
                        },
                        CreatedAt = a.CreatedAt,
                        Price = a.Price,
                        ExchangeGames = a.ExchangeGames.Select(g => new
                        {
                            g.Game.Name,
                            g.Game.Avatar
                        })

                    }).AsEnumerable().Select(v => new
                    {
                        Game = v.Game,
                        Avatar = Url.Content(v.Avatar),
                        Caption = v.Caption,
                        MedType = v.MedType,
                        GameRegion = v.GameRegion,
                        Location = v.Location,
                        Platform = v.Platform,
                        Price = v.Price,
                        CreateAt = v.CreatedAt,
                        ExchangeGames = v.ExchangeGames.Select(g => new
                        {
                            Name = g.Name,
                            Avatar = Url.Content(g.Avatar.ImagePath)
                        })
                    }).ToList();
                return Ok(final);
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
                                                          s.State==SessionState.Actived);
                if (session != null)
                {
                    var user = session.User;
                    Models.Image imageInDb = null;

                    if (!string.IsNullOrEmpty(advertisementCreate.UserImage))
                    {
                        Image image;
                        var byteArray = Convert.FromBase64String(advertisementCreate.UserImage);
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Image/Advertisements/" + user.Id));
                        string folderPath = HttpContext.Current.Server.MapPath("~/Image/Advertisements/" + user.Id+"/");
                        string fileName = "UserImage" + "_" + DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") + ".jpg";
                        string imagePath = folderPath + fileName;
                        string dbPath = "~/Image/Advertisements/" + session.User.Id + fileName;
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
                                                          s.State == SessionState.Actived);
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
                    Platform = f.Platform.Name,
                    Similar = db.Advertisements.Where(a=>a.GameId==f.GameId)

                });

            return NotFound();
        }*/
    }
}