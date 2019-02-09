using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teeleh.Models.Enums
{
    ///////////////////////////////////////////////////// Advertisement Enums //////////////////////////////////////////
    public enum MediaType
    {
        NEW,
        SECOND_HAND,
    }

    public enum GameRegion
    {
        ALL,
        R1,
        R2,
        R3,
        R4
    }

    ///////////////////////////////////////////////////// Request Enums //////////////////////////////////////////////
    
    public enum FilterType
    {
        ALL,
        JUST_NEW,
        JUST_SECOND_HAND,
    }
    public enum RequestMode
    {
        ALL,
        JUST_SELL,
        JUST_EXCHANGE
    }

    ///////////////////////////////////////////////////// Image Enums //////////////////////////////////////////////
    
    public enum ImageType
    {
        AVATAR,
        COVER,
        USER_IMAGE
    }

    ///////////////////////////////////////////////////// Filter Enums //////////////////////////////////////////////
    
    public enum Sort
    {
        PRICE_ASCENDING,
        PRICE_DESCENDING,
        NEWEST
    }

    ///////////////////////////////////////////////////// Game Enums //////////////////////////////////////////////

    public enum ESRB
    {
        [Display(Name = "E")]
        E = 1,
        [Display(Name = "E +10")]
        E10 = 2,
        [Display(Name = "T")]
        T = 3,
        [Display(Name = "M")]
        M = 4,
        [Display(Name = "AO")]
        AO = 5
    }

    ///////////////////////////////////////////////////// Session Enums //////////////////////////////////////////////

    public enum SessionState { PENDING, ACTIVE, DEACTIVE, ABOLISHED }

    public enum SessionPlatform { ANDROID, IOS, WEB }


    //////////////////////////////////////////////////// Notifications ///////////////////////////////////////////////

    public enum NotificationStatus
    {
        SEEN,
        UNSEEN
    }

}
