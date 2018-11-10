using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teeleh.Models
{
    public class Exchange
    {
        public Game Game { get; set; }
        public Advertisement Advertisement { get; set; }

        [Key]
        [Column(Order = 1)]
        public int AdvertisementId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int GameId { get; set; }
    }
}
