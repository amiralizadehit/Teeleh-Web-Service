using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Teeleh.Models;
using Teeleh.Models.ViewModels;

namespace Teeleh.WApi.Helper
{
    public class QueryHelper
    {
        private static string localDomain = "http://" + HttpContext.Current.Request.Url.Host;


        public static string GetLocalDomain()
        {
            return localDomain;
        }


        ///////////////////// Advertisements Queries /////////////////////////////////////

        public static Expression<Func<Advertisement, object>> GetAdvertisementQuery()
        {
            return a => new
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
                Age = SqlFunctions.DateDiff("minute", a.CreatedAt, DateTime.Now),
                Price = a.Price,
                ExchangeGames = a.ExchangeGames.Select(g => new
                {
                    Name = g.Game.Name,
                    Avatar = localDomain + g.Game.Avatar.ImagePath
                })
            };
        }

        public static Expression<Func<Advertisement, bool>> GetAdvertisementValidationQuery()
        {
            return g => g.isDeleted == false;
        }

        ///////////////////// Requests Queries //////////////////////////////////////////

        public static Expression<Func<Request, object>> GetRequestQuery()
        {
            return r => new
            {
                r.Id,
                Game = r.Game.Name,
                Avatar = localDomain + r.Game.Avatar.ImagePath,
                Platform = r.Platforms.Select(p => p.Id),
                Location = new
                {
                    Province = r.LocationProvince,
                    City = r.LocationCity,
                    Region = r.LocationRegion
                },
                MinPrice = r.MinPrice,
                MaxPrice = r.MaxPrice,
                IsDeleted = r.IsDeleted,
                FilterType = r.FilterType,
                RequestMode = r.ReqMode,
                CreatedAt = r.CreatedAt,
                UpdateAt = r.UpdatedAt
            };
        }

        public static Expression<Func<Request, bool>> GetRequestValidationQuery()
        {
            return g => g.IsDeleted == false;
        }

        //////////////////// Sessions Queries ///////////////////////////////////////////

        public static Expression<Func<Session, object>> GetSessionQuery()
        {
            return s => new
            {
                s.Id,
                s.SessionKey,
                s.ActivationMoment,
                s.DeactivationMoment,
                s.State,
                s.UniqueCode,
                User = new
                {
                    s.User.FirstName,
                    s.User.LastName,
                    s.User.CreatedAt,
                    s.User.Email,
                    s.User.PhoneNumber,
                    s.User.PSNId,
                    s.User.XBOXLive,
                    s.User.State
                }
            };
        }

        public static Expression<Func<Session, bool>> GetSessionValidationQuery(SessionInfoObject seesionInfo)
        {
            return s => s.SessionKey == seesionInfo.SessionKey &&
                        s.Id == seesionInfo.SessionId &&
                        s.State == SessionState.Active;
        }

        public static Expression<Func<Session, bool>> GetPendingSessionQuery(PendingSessionViewModel pendingSession)
        {
            return s => s.Id == pendingSession.SessionId
                        && s.Nonce == pendingSession.Nounce
                        && s.State == SessionState.Pending;
        }

        //////////////////// Games Queries //////////////////////////////////////////////

        public static Expression<Func<Game, object>> GetGameQuery()
        {
            return x => new
            {
                Id = x.Id,
                Name = x.Name,
                Image = localDomain + x.Avatar.ImagePath,
                UserScore = x.UserScore,
                MetaScore = x.MetaScore,
                OnlineCapability = x.OnlineCapability,
                Publisher = x.Publisher,
                Developer = x.Developer,
                ReleaseDate = x.ReleaseDate,
                Genres = x.Genres.Select(t => t.Name).ToList(),
                Platforms = x.SupportedPlatforms.Select(p => p.Name).ToList()
            };
        }

        //////////////////// Locations Queries //////////////////////////////////////////

        public static Expression<Func<Location, object>> GetLocationQuery()
        {
            return b => new
            {
                Id = b.Id,
                Name = b.Name
            };
        }

        public static Expression<Func<Location, bool>> GetLocationValidationQuery(int? id, Location.LocationType type)
        {
            return l => l.ParentId == id && l.Type == type;
        }

        public static Expression<Func<Location, bool>> GetLocationValidationQuery(Location.LocationType type)
        {
            return l => l.Type == type;
        }

        //////////////////// User Queries //////////////////////////////////////////

        public static Expression<Func<User, object>> GetUserQuery()
        {
            return q => new
            {
                q.FirstName,
                q.LastName,
                q.CreatedAt,
                q.Email,
                q.PhoneNumber,
                q.PSNId,
                q.XBOXLive,
                q.State,
                Sessions = q.Sessions.Select(s => new
                {
                    s.ActivationMoment,
                    s.DeactivationMoment,
                    s.UniqueCode,
                    s.State,
                })
            };
        }
    }
}