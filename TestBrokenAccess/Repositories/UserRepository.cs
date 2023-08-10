using TestBrokenAccess.Data;
using TestBrokenAccess.Models;
using TestBrokenAccess.Utils;

namespace TestBrokenAccess.Repositories
{

    public class UserRepository : IUserRepository
    {
        private TestBrokenAccessContext _db;

        public UserRepository(TestBrokenAccessContext db)
        {
            _db = db;
        }

        public CookieUserItem Validate(LoginModel model)
        {
            var emailRecords = _db.Users.Where(x => x.EmailAddress == model.EmailAddress);

            var results = emailRecords.AsEnumerable()
            .Where(m => m.PasswordHash == Hasher.GenerateHash(model.Password, m.Salt))
            .Select(m => new CookieUserItem
            {
                UserId = m.Id,
                EmailAddress = m.EmailAddress,
                Name = m.Name,
                CreatedUtc = m.CreatedUtc,
                Role = m.Role
            });

            return results.FirstOrDefault();
        }

        public CookieUserItem Register(RegisterModel model)
        {
            var salt = Hasher.GenerateSalt();
            var hashedPassword = Hasher.GenerateHash(model.Password, salt);

            var existedUser = _db.Users.Any(user => user.EmailAddress.Equals(model.EmailAddress.Trim()));
            if (existedUser) { return null; }


            var user = new CookieUser
            {
                Id = Guid.NewGuid(),
                EmailAddress = model.EmailAddress.Trim(),
                PasswordHash = hashedPassword,
                Salt = salt,
                Name = model.Name.Trim(),
                CreatedUtc = DateTime.UtcNow,
                Role = RoleType.User
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            return new CookieUserItem
            {
                UserId = user.Id,
                EmailAddress = user.EmailAddress,
                Name = user.Name,
                CreatedUtc = user.CreatedUtc,
                Role = user.Role
            };
        }

        public List<CookieUserItem> GetListUsers()
        {
            return _db.Users.Select(item => new CookieUserItem()
            {
                UserId = item.Id,
                EmailAddress = item.EmailAddress,
                Name = item.Name,
                CreatedUtc = item.CreatedUtc,
                Role = item.Role
            }).ToList();
        }

        public CookieUserItem GetUserInfo(string email)
        {
            CookieUser user =  _db.Users.FirstOrDefault(item => item.EmailAddress.Equals(email));
            if(user != null)
            {
                return new CookieUserItem()
                {
                    UserId = user.Id,
                    EmailAddress = user.EmailAddress,
                    Name = user.Name,
                    CreatedUtc = user.CreatedUtc,
                    Role = user.Role
                };
            }
            return null;
        }
    }
}
