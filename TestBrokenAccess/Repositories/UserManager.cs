
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TestBrokenAccess.Models;

namespace TestBrokenAccess.Repositories
{
    public class UserManager : IUserManager
    {
        public async Task SignIn(HttpContext context, CookieUserItem user, bool isPersistent = false)
        {
            string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            var claims = GetUserClaims(user);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, authenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperty = new AuthenticationProperties()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                IsPersistent = isPersistent,
                RedirectUri = "~/Account/Index",
            };

            await context.SignInAsync(authenticationScheme, claimsPrincipal, authProperty);
        }

        public async Task SignOut(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }


        private List<Claim> GetUserClaims(CookieUserItem user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

            return claims;
        }
    }
}
