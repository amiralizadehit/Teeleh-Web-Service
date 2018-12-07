using System.ComponentModel;

namespace Teeleh.Models.ViewModels.Website_View_Models
{
    public class AdminLoginViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
