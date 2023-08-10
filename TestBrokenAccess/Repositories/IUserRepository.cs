using TestBrokenAccess.Models;

namespace TestBrokenAccess.Repositories
{
    public interface IUserRepository
    {
        CookieUserItem Register(RegisterModel model);
        CookieUserItem Validate(LoginModel model);
        public List<CookieUserItem> GetListUsers();
        public CookieUserItem GetUserInfo(Guid userId);
    }
}
