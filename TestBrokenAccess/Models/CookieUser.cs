using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TestBrokenAccess.Models
{
    public class CookieUser
    {
        [Key]
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.Now;
        public RoleType Role { get; set; }
    }

    public enum RoleType 
    {
        Admin,
        User
    }

}
