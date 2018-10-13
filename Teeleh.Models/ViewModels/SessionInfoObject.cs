using System.ComponentModel.DataAnnotations;

namespace Teeleh.Models.ViewModels
{
    public class SessionInfoObject
    {
        [Required]
        public string SessionKey { get; set; }

        [Required]
        public int SessionId { get; set; }
    }
}
