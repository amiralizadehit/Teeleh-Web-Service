using System.ComponentModel.DataAnnotations;

namespace Teeleh.Models.ViewModels
{
    public class DeactiveSessionViewModel
    {
        [Required]
        public int SessionId { get; set; }

        [Required]
        [Range(10000,99999)]
        public string SessionKey { get; set; }
    }
}
