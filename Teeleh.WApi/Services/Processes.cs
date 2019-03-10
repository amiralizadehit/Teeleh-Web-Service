using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Teeleh.Models;
using Teeleh.Models.Enums;
using Teeleh.Utilities;
using Teeleh.WApi.Helper;

namespace Teeleh.WApi.Services
{
    public class Processes
    {
        private AppDbContext db;
        public Processes()
        {
            db = new AppDbContext();
        }

        public void SendNotification()
        {
            var notifications = db.Notifications.Where(n => n.Status == NotificationStatus.UNSEEN);

            if (notifications.Any())
            {
                foreach (var notification in notifications)
                {
                    var userSessions = notification.User.Sessions;
                
                    foreach (var userSession in userSessions)
                    {
                        //we want to make sure that we send notification to an active session
                        if (userSession.State == SessionState.ACTIVE)
                        {
                            var fcmToken = userSession.FCMToken;
                            if (fcmToken != null)
                            {
                                var game = db.Games.Single(g => g.Id == notification.Advertisement.GameId);
                                NotificationHelper.SendRequestNotification(fcmToken, game.Avatar.ImagePath, game.Name, notification.AdvertisementId);
                            }
                        }
                        
                        
                    }
                
                }
            }
        }

        public void UpdateRecommenderSystem()
        {



            //var advertisementInDb = await db.Advertisements.Where(QueryHelper.GetAdvertisementValidationQuery())
            //    .SingleOrDefaultAsync(c => c.Id == id);
            //if (advertisementInDb != null)
            //{
            //    var game = advertisementInDb.Game;
            //    var toQuery = db.Advertisements.Where(QueryHelper.GetAdvertisementValidationQuery())
            //        .Where(a => a.Id != id);
            //    var similarAds = toQuery.Where(a => a.Game.Id == game.Id).Take(10);
            //    if (similarAds.Count() < 4)
            //    {
            //        var numLeft = 4 - similarAds.Count();
            //        var toAdd = toQuery.Where(a => a.LocationCityId == advertisementInDb.LocationCityId).Take(numLeft);
            //        similarAds = similarAds.Concat(toAdd);
            //        if (toAdd.Count() < numLeft)
            //        {
            //            var numLeft2 = numLeft - toAdd.Count();
            //            var toAdd2 = toQuery.Where(a => a.Game.Developer == game.Developer).Take(numLeft2);
            //            similarAds = similarAds.Concat(toAdd2);
            //        }
            //    }

            //    var result = similarAds.Select(QueryHelper.GetAdvertisementQuery()).ToList();
                
            //}
        }

    }
}