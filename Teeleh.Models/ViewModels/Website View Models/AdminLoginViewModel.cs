using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Cache;

namespace Teeleh.Models.ViewModels.Website_View_Models
{
    public class AdminLoginViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        
        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}

