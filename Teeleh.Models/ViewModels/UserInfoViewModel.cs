namespace Teeleh.Models.ViewModels
{
    public class UserInfoViewModel : SessionInfoObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] UserAvatar { get; set; }
        public string PSNId { get; set; }
        public string XBOXLive { get; set; }

    }

}

