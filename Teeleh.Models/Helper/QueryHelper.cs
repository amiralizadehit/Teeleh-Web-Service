using System;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Teeleh.Models.Dtos;
using Teeleh.Models.Enums;
using Teeleh.Models.ViewModels;

namespace Teeleh.Models.Helper
{
    public class QueryHelper
    {
        private static string localDomain = "http://" + HttpContext.Current.Request.Url.Host;


        public static string GetLocalDomain()
        {
            return localDomain;
        }


        ///////////////////// Advertisements Queries /////////////////////////////////////

        public static Expression<System.Func<Advertisement, object>> GetAdvertisementQuery()
        {

            return a => new
            {
                Id = a.Id,
                Game = a.Game.Name,
                Avatar = localDomain + a.Game.Avatar.ImagePath,
                Cover = localDomain + a.Game.Cover.ImagePath,
                UserImage = localDomain + a.UserImage.ImagePath,
                Platform = a.Platform.Name,
                MedType = a.MedType,
                Caption = a.Caption,
                GameRegion = a.GameReg,
                Location = new
                {
                    ProvinceId = a.LocationProvinceId,
                    Province = a.LocationProvince.Name,
                    CityId = a.LocationCityId,
                    City = a.LocationCity.Name,
                    RegionId = a.LocationRegionId,
                    Region = a.LocationRegion.Name,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude
                },
                CreatedAt = a.CreatedAt,
                Age = SqlFunctions.DateDiff("minute", a.CreatedAt, DateTime.Now),
                Price = a.Price,
                Information = new{
                    PhoneNumber = a.User.PhoneNumber,
                    Email = a.User.Email,
            },
                ExchangeGames = a.ExchangeGames.Where(e=>e.Game.isDeleted==false).Select(g => new
                {
                    Id = g.GameId,
                    Name = g.Game.Name,
                    Avatar = localDomain + g.Game.Avatar.ImagePath
                })
            };
        }

        public static Expression<System.Func<Advertisement, bool>> GetAdvertisementValidationQuery()
        {
            return g => g.isDeleted == false &&
                        g.Game.isDeleted == false &&
                        g.User.State==UserState.ACTIVE;
        }


        ///////////////////// Requests Queries //////////////////////////////////////////

        public static Expression<System.Func<Request, object>> GetRequestQuery()
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

        public static Expression<System.Func<Request, bool>> GetRequestValidationQuery()
        {
            return g => g.IsDeleted == false &&
                        g.Game.isDeleted == false &&
                        g.User.State==UserState.ACTIVE;
        }

        //////////////////// Sessions Queries ///////////////////////////////////////////

        public static Expression<System.Func<Session, object>> GetSessionQuery()
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

        public static Expression<System.Func<Session, bool>> GetSessionObjectValidationQuery(SessionInfoObject seesionInfo)
        {
            return s => s.SessionKey == seesionInfo.SessionKey &&
                        s.Id == seesionInfo.SessionId &&
                        s.State == SessionState.ACTIVE &&
                        s.User.State ==UserState.ACTIVE;
        }

        public static Expression<System.Func<Session, bool>> GetSessionValidationQuery()
        {
            return g => g.State ==SessionState.ACTIVE;
        }

        public static Expression<System.Func<Session, bool>> GetPendingSessionQuery(NoncePairDto pendingSession)
        {
            return s => s.Id == pendingSession.Session.SessionId &&
                        s.SessionKey == pendingSession.Session.SessionKey
                        && s.Nonce == pendingSession.Nonce
                        && s.State == SessionState.PENDING;
        }

        //////////////////// Games Queries //////////////////////////////////////////////

        public static Expression<System.Func<Game, object>> GetGameQuery()
        {
            return x => new
            {
                Id = x.Id,
                Name = x.Name,
                Image = localDomain + x.Avatar.ImagePath,
                CoverImage = localDomain + x.Cover.ImagePath,
                GameplayImages = x.GameplayImages.Select(g=>localDomain+g.ImagePath).ToList(),
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

        public static Expression<System.Func<Game, bool>> GetGameValidationQuery()
        {
            return g => g.isDeleted == false;
        }


        //////////////////// Locations Queries //////////////////////////////////////////

        public static Expression<System.Func<Location, object>> GetLocationQuery()
        {
            return b => new
            {
                Id = b.Id,
                Name = b.Name
            };
        }

        public static Expression<System.Func<Location, bool>> GetLocationValidationQuery(int? id, Location.LocationType type)
        {
            return l => l.ParentId == id && l.Type == type;
        }

        public static Expression<System.Func<Location, bool>> GetLocationValidationQuery(Location.LocationType type)
        {
            return l => l.Type == type;
        }

        //////////////////// Users Queries //////////////////////////////////////////

        public static Expression<System.Func<User, object>> GetUserQuery()
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

        public static Expression<System.Func<User, bool>> GetUserValidationQuery()
        {
            return u => u.State == UserState.ACTIVE;
        }


        //////////////////// AdBookmarks Queries //////////////////////////////////////

        public static Expression<System.Func<AdBookmark, bool>> GetAdBookmarksValidationQuery(int userId)
        {
            //validation and authorization
            return b => b.IsDeleted == false 
                        && b.UserId==userId;
        }
    }
}