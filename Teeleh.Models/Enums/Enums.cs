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
    ///////////////////////////////////////////////////// PSN Account //////////////////////////////////////////////////

    public enum PSNAccountCapacity
    {
        ONE,
        TWO,
        THREE
    }
    public enum PSNAccountType
    {
        LEGAL,
        COMBINATIONAL
    }

    public enum PSNAccountRegion
    {
        R1,
        R2,
        R3
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
        USER_IMAGE,
        GAMEPLAY,
        NOTIFICATION_IMAGE
    }

    public enum WebImageType
    {
        AVATAR,
        COVER,
        GAMEPLAY,
        NOTIFICATION_IMAGE
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

    ///////////////////////////////////////////////////// Session & User Enums //////////////////////////////////////////////
    
    public enum SessionState { PENDING, ACTIVE, DEACTIVE, ABOLISHED }

    public enum UserState { PENDING, ACTIVE, DELETED, SUSPENDED }

    public enum SessionPlatform { ANDROID, IOS, WEB }


    ///////////////////////////////////////////////////// Admin Page ///////////////////////////////////////////////////////
    
    public enum GameplayImageOption { MAKE_NEW, ADD_TO_EXISTING}

    //////////////////////////////////////////////////// Notifications ////////////////////////////////////////////////////

    public enum NotificationStatus
    {
        NEW,
        SENT,
        SEEN,
        DELETED
    }

    public enum NotificationType
    {
        CASUAL,
        ADVERTISEMENT
    }
    ///////////////////////////////////////////////////// Location /////////////////////////////////////////////////////////
    public enum LocationType
    {
        PROVINCE,
        CITY,
        REGION
    }
}
