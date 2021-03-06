﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Teeleh.Models;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Models.Helper;
using Teeleh.Models.ViewModels;
using Teeleh.Utilities;
using Teeleh.WApi.Functions;
using Image = System.Drawing.Image;

namespace Teeleh.WApi.Controllers
{
    /// <summary>
    /// This class is used to manage client requests related to Disk Advertisements.
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
            var advertisements = db.Advertisements.Where(a=>!a.Game.isDeleted && a.User.State==UserState.ACTIVE)
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
        public IHttpActionResult GetAdvertisements(FeedOptions options)
        {
            if (ModelState.IsValid)
            {
                var pageSize = 10;
                var platforms = options.Filter.PlatformIds;
                var minPrice = (options.Filter.MinPrice ?? 0);
                var maxPrice = (options.Filter.MaxPrice ?? 3000000);
                var provinceId = options.Filter.LocationProvinceId;
                var cityId = options.Filter.LocationCityId;
                var mediaType = options.Filter.MedType;
                var pageNumber = (options.Filter.PageNumber ?? 1);
                var search = options.Search;
                var sort = options.Sort;

                var query = db.Advertisements.Where(a => a.Price >= minPrice && a.Price <= maxPrice)
                    .Where(QueryHelper.GetAdvertisementValidationQuery());


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

                if (!search.IsNullOrWhiteSpace())
                {
                    search = search.TrimEnd(' ');
                    query = query.Where(s => s.Game.Name.Contains(search));
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
                    db.Sessions.SingleOrDefaultAsync(
                        QueryHelper.GetSessionObjectValidationQuery(advertisementCreate.Session));
                if (session != null)
                {
                    var user = session.User;


                    var isImageEmpty = string.IsNullOrEmpty(advertisementCreate.UserImage);

                    Models.Image userImage = null;
                    if (!isImageEmpty)
                    {
                        //we write user's uploaded image in memory
                        userImage = ImageHandler.CreateUserImage(db, user.Id, advertisementCreate.UserImage);
                    }

                    var newAdvertisement = new Advertisement()
                    {
                        User = user,
                        MedType = advertisementCreate.MedType,
                        GameId = advertisementCreate.GameId,
                        GameReg = advertisementCreate.GameReg,
                        Latitude = advertisementCreate.Latitude,
                        Longitude = advertisementCreate.Longitude,
                        LocationRegionId = advertisementCreate.LocationRegionId,
                        LocationCityId = advertisementCreate.LocationCityId,
                        LocationProvinceId = advertisementCreate.LocationProvinceId,
                        Price = advertisementCreate.Price,
                        PlatformId = advertisementCreate.PlatformId,
                        Caption = advertisementCreate.Caption,
                        UserImage = userImage,
                        isDeleted = false,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    db.Advertisements.Add(newAdvertisement);


                    if (advertisementCreate.ExchangeGames.Count > 0) //we have some games to exchange
                    {
                        foreach (var game in advertisementCreate.ExchangeGames)
                        {
                            var newExchange = new Exchange()
                            {
                                AdvertisementId = newAdvertisement.Id,
                                GameId = game
                            };
                            db.Exchanges.Add(newExchange);
                        }
                    }

                     await db.SaveChangesAsync();
                    // Broadcasting

                    NotificationGenerator.NewAdvertisementNotification(db, newAdvertisement);


                    return Ok(newAdvertisement.Id);
                }

                return Unauthorized();
            }

            return BadRequest();
        }

        /// <summary>
        /// This endpoint edits an advertisement
        /// </summary>
        /// <returns>200 : Ok |
        /// 401 : Session info not found |
        /// 400 : Bad Request |
        /// 404 : Advertisement not found
        /// </returns>
        [HttpPost]
        [Route("api/advertisements/edit")]
        public async Task<IHttpActionResult> Edit(AdvertisementEditDto editAd)
        {
            if (ModelState.IsValid)
            {
                var session = await
                    db.Sessions.SingleOrDefaultAsync(
                        QueryHelper.GetSessionObjectValidationQuery(editAd.Session));
                if (session != null)
                {
                    var user = session.User;
                    var adInDb =
                        db.Advertisements.Include(d => d.UserImage)
                            .SingleOrDefault(a => a.Id == editAd.Id && a.User.Id == user.Id);

                    if (adInDb != null)
                    {
                        var isImageEmpty = string.IsNullOrEmpty(editAd.UserImage);

                        //we check if a user has lowered the price of an advertisement to some degree.
                        var isHot = editAd.Price <= adInDb.Price * 3 / 4;

                        //We want to know if a user has uploaded a new image for his/her advertisement 
                        if (!isImageEmpty)
                        {
                            adInDb.UserImage = ImageHandler.CreateUserImage(db, user.Id, editAd.UserImage);
                        }
                        else
                        {
                            adInDb.UserImage = null;
                        }

                        adInDb.MedType = editAd.MedType;
                        adInDb.Latitude = editAd.Latitude;
                        adInDb.Longitude = editAd.Longitude;
                        adInDb.LocationRegionId = editAd.LocationRegionId;
                        adInDb.LocationCityId = editAd.LocationCityId;
                        adInDb.LocationProvinceId = editAd.LocationProvinceId;
                        adInDb.Price = editAd.Price;
                        adInDb.Caption = editAd.Caption;
                        adInDb.UpdatedAt = DateTime.Now;

                        //first we have to get rid of old exchange records in database
                        db.Exchanges.RemoveRange(db.Exchanges.Where(x => x.AdvertisementId == adInDb.Id));

                        if (editAd.ExchangeGames.Count > 0) //we have some games to exchange
                        {
                            foreach (var game in editAd.ExchangeGames)
                            {
                                var newExchange = new Exchange()
                                {
                                    AdvertisementId = adInDb.Id,
                                    GameId = game
                                };
                                db.Exchanges.Add(newExchange);
                            }
                        }


                        await db.SaveChangesAsync();

                        // we re-broadcast this advertisement
                        NotificationGenerator.OldAdvertisementNotification(db, adInDb, isHot);

                        return Ok();
                    }

                    return NotFound();
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
        [Route("api/advertisements/delete")]
        public async Task<IHttpActionResult> Delete(IDPairDto pair)
        {
            if (ModelState.IsValid)
            {
                var sessionInDb = await
                    db.Sessions.SingleOrDefaultAsync(QueryHelper.GetSessionObjectValidationQuery(pair.Session));

                if (sessionInDb != null)
                {
                    var userId = sessionInDb.User.Id;
                    var advertisementInDb =
                        db.Advertisements.SingleOrDefault(a => a.Id == pair.Id && a.User.Id == userId);
                    if (advertisementInDb != null)
                    {
                        advertisementInDb.isDeleted = true;
                        db.SaveChanges();
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
        /// <returns>200 : Ok |
        /// 404 : Advertisement Not Found 
        /// </returns>
        [HttpGet]
        [Route("api/advertisements/similar/{id}")]
        public async Task<IHttpActionResult> GetSimilarAdvertisements(int id) //Some dummy algorithm has been implemented
        {
            var advertisementInDb = await db.Advertisements.Where(QueryHelper.GetAdvertisementValidationQuery())
                .SingleOrDefaultAsync(c => c.Id == id);
            if (advertisementInDb != null)
            {
                var game = advertisementInDb.Game;
                var toQuery = db.Advertisements.Where(QueryHelper.GetAdvertisementValidationQuery())
                    .Where(a => a.Id != id);
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


        /// <summary>
        /// Returns all information of the advertisement specified with given id.
        /// </summary>
        /// <returns>200 : Ok |
        /// 404 : Advertisement Not Found 
        /// </returns>
        [HttpGet]
        [Route("api/advertisements/detail/{id}")]
        public IHttpActionResult Detail(int id)
        {
            var advertisementInDb = db.Advertisements
                .Where(g => g.Id == id)
                .Where(a => !a.Game.isDeleted && a.User.State == UserState.ACTIVE);
                

            if (advertisementInDb.Any())
            {
                return Ok(advertisementInDb.Select(QueryHelper.GetAdvertisementQuery()));
            }
            return NotFound();
        }
    }   
}