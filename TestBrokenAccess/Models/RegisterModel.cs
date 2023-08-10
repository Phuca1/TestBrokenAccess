using System.ComponentModel.DataAnnotations;

namespace TestBrokenAccess.Models
{
    public class RegisterModel : LoginModel
    {
        public string Name { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
