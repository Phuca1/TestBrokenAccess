namespace TestBrokenAccess.Models
{
    public class CookieUserItem
    {
        public Guid UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public DateTime CreatedUtc { get; set; }
        public RoleType Role { get; set; }
    }
}
