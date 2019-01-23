using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Teeleh.Models;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Models.ViewModels;
using Teeleh.Utilities;
using Teeleh.WApi.Helper;
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
        /// This endpoint returns a list of all advertisements.
        /// </summary>
        /// <returns>200 : sent
        /// </returns>
        [HttpGet]
        [Route("api/advertisements")]
        public IHttpActionResult GetAdvertisements()
        {
            
            var advertisements = db.Advertisements.Where(QueryHelper.GetAdvertisementValidationQuery())
                .Select(QueryHelper.GetAdvertisementQuery()).ToList();

            return Ok(advertisements);
        }

        /// <summary>
        /// This endpoint returns a list of advertisements filtered by given filter object.
        /// </summary>
        /// <returns>200 : sent |
        /// 400 : Bad Request
        /// </returns>
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
                    .Select(QueryHelper.GetAdvertisementQuery()).ToList();
                return Ok(advertisements);
            }

            return BadRequest();
        }


        /// <summary>
        /// This endpoint returns a list of advertisements owned by a user specified by given session information.
        /// </summary>
        /// <returns>200 : sent |
        /// 400 : Bad Request |
        /// 401 : Session info not found
        /// </returns>
        [HttpPost]
        [Route("api/advertisements/me")]
        public async Task<IHttpActionResult> GetAdvertisements(SessionInfoObject sessionInfoObject)
        {
            if (ModelState.IsValid)
            {
                var sessionInDb = await
                    db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionValidationQuery(sessionInfoObject));
                if (sessionInDb != null)
                {
                    var user = sessionInDb.User;
                    var advertisement = db.Advertisements
                        .Where(QueryHelper.GetAdvertisementValidationQuery()).Where(c=>c.User.Id == user.Id)
                        .Select(QueryHelper.GetAdvertisementQuery()).ToList();
                    return Ok(advertisement);

                }
                return Unauthorized();
            }
            return BadRequest();
        }


        /// <summary>
        /// This endpoint creates an advertisement with the given information.
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
                    db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionValidationQuery(advertisementCreate.SessionInfo));
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
                            Type = ImageType.USER_IMAGE,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };
                        db.Images.Add(imageInDb);
                        await db.SaveChangesAsync();
                    }

                    var new_advertisement = new Advertisement()
                    {
                        User = user,
                        MedType = (MediaType) advertisementCreate.MedType,
                        GameId = advertisementCreate.GameId,
                        GameReg = (GameRegion) advertisementCreate.GameReg,
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

                    // Broadcasting

                    new Thread(delegate () {
                        Broadcast(advertisementCreate);
                    }).Start();


                    return Ok(new_advertisement.Id);
                }

                return Unauthorized();
            }

            return BadRequest();
        }

        /// <summary>
        /// This endpoint deletes an advertisement with given id.
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
                    db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionValidationQuery(pair.session));
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
        [Route("api/advertisements/detail/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Detail(int id)                 //Some dummy algorithm has been implemented
        {
            var advertisementInDb = await db.Advertisements.Where(QueryHelper.GetAdvertisementValidationQuery()).SingleOrDefaultAsync(c=>c.Id==id);

            if (advertisementInDb != null)
            {
                var game = advertisementInDb.Game;
                var toQuery = db.Advertisements.Where(QueryHelper.GetAdvertisementValidationQuery()).Where(a=> a.Id != id);
                var similarAds = toQuery.Where(a => a.Game.Id == game.Id).Take(10);
                if (similarAds.Count() < 4)
                {
                    var numLeft = 4 - similarAds.Count();
                    var toAdd = toQuery.Where(a => a.LocationCityId == advertisementInDb.LocationCityId).Take(numLeft);
                    similarAds = similarAds.Concat(toAdd);
                    if (toAdd.Count() < numLeft)
                    {
                        var numLeft2 = numLeft - toAdd.Count();
                        var toAdd2 = toQuery.Where(a => a.Game.Developer == game.Developer).Take(numLeft2);
                        similarAds = similarAds.Concat(toAdd2);
                    }
                }

                var result = similarAds.Select(QueryHelper.GetAdvertisementQuery()).ToList();
                return Ok(result);
            }
                
            return NotFound();
        }



        private void Broadcast(AdvertisementCreateDto advertisementCreate)
        {
            var adPrice = advertisementCreate.Price;
            var exchangeGamesCounts = advertisementCreate.ExchangeGames.Count;

            var query = db.Requests.Where(QueryHelper.GetRequestValidationQuery())
                .Where(s => s.GameId == advertisementCreate.GameId);

            if (adPrice != 0 && exchangeGamesCounts > 0)
            {
                query = query.Where(r => r.ReqMode == RequestMode.ALL);
            }
            else if (adPrice == 0 && exchangeGamesCounts > 0)
            {
                query = query.Where(r => r.ReqMode == RequestMode.JUST_EXCHANGE);
            }
            else
            {
                query = query.Where(r => r.ReqMode == RequestMode.JUST_SELL);
            }

            if ((MediaType)advertisementCreate.MedType == MediaType.NEW)
            {
                query = query.Where(r => r.FilterType == FilterType.JUST_NEW);
            }
            else
            {
                query = query.Where(r => r.FilterType == FilterType.JUST_SECOND_HAND);
            }

            if (query.Any())
            {
                foreach (var request in query)
                {
                    var requestUser = request.User;
                    var userSessions = requestUser.Sessions;
                    foreach (var userSession in userSessions)
                    {
                        if (userSession.State == SessionState.ACTIVE)
                        {
                            var fcmToken = userSession.FCMToken;
                            if (fcmToken != null)
                            {
                                NotificationHelper.SendNotification(fcmToken);
                            }
                        }
                    }

                }
            }
        }
    }
}