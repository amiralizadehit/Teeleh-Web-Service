using System.ComponentModel.DataAnnotations;

namespace Teeleh.Models.ViewModels
{
    public class PendingSessionViewModel
    {
        [Required]
        public int SessionId { get; set; }

        [Required]
        [Range(10000,99999)]
        public int Nounce { get; set; }
    }
}
