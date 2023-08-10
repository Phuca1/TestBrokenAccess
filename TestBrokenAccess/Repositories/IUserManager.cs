
using TestBrokenAccess.Models;

namespace TestBrokenAccess.Repositories
{
    public interface IUserManager
    {
        Task SignIn(HttpContext context, CookieUserItem user, bool isPersistent =false);
        Task SignOut(HttpContext context);
    }
}
