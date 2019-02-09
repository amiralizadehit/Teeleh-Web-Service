using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teeleh.Models;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Utilities;
using Teeleh.WApi.Helper;

namespace Teeleh.WApi.Functions
{
    public class NotificationHandler
    {
        public static void CheckForNotification(AppDbContext db, AdvertisementCreateDto advertisementCreate, int advertisementId)
        {
            var adPrice = advertisementCreate.Price;
            var exchangeGamesCounts = advertisementCreate.ExchangeGames.Count;

            var query = db.Requests.Where(QueryHelper.GetRequestValidationQuery())
                .Where(s => s.GameId == advertisementCreate.GameId);

            if (adPrice != 0 && exchangeGamesCounts > 0)
            {
                query = query.Where(r => r.ReqMode == RequestMode.ALL)
                             .Where(p => p.MinPrice <= adPrice && p.MaxPrice >= adPrice);

            }
            else if (adPrice == 0 && exchangeGamesCounts > 0)
            {
                query = query.Where(r => r.ReqMode == RequestMode.JUST_EXCHANGE);
            }
            else
            {
                query = query.Where(r => r.ReqMode == RequestMode.JUST_SELL)
                             .Where(p => p.MinPrice <= adPrice && p.MaxPrice >= adPrice);
            }

            query = (MediaType)advertisementCreate.MedType == MediaType.NEW 
                ? query.Where(r => r.FilterType == FilterType.JUST_NEW) 
                : query.Where(r => r.FilterType == FilterType.JUST_SECOND_HAND);

            if (!query.Any()) return;
            foreach (var request in query)
            {
                /*var userSessions = request.User.Sessions;
                foreach (var userSession in userSessions)
                {
                    if (userSession.State != SessionState.ACTIVE) continue;
                    var fcmToken = userSession.FCMToken;
                    if (fcmToken == null) continue;
                    var game = db.Games.Single(g => g.Id == advertisementCreate.GameId);
                    NotificationHelper.SendRequestNotification(fcmToken, game.Avatar.ImagePath, game.Name, advertisementId);
                }*/
                var userId = request.User.Id;
                var notification = new Notification()
                {
                    AdvertisementId = advertisementId,
                    CreatedAt = DateTime.Now,
                    UserId = userId,
                    Status = NotificationStatus.UNSEEN
                };
                db.Notifications.Add(notification);
            }

            db.SaveChanges();
        }
    }
}