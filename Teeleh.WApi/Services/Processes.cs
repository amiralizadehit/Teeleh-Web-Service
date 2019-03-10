using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Teeleh.Models;
using Teeleh.Models.Enums;
using Teeleh.Utilities;

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
            throw new NotImplementedException();
        }

    }
}