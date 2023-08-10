using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestBrokenAccess.Models;
using TestBrokenAccess.Repositories;
using System.Web;
using System.Net;

namespace TestBrokenAccess.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserManager _userManager;
        public IActionResult Index()
        {
            return View();
        }

        public AccountController(IUserRepository userRepository, IUserManager userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            Console.WriteLine(this.HttpContext.User.Identities);
            return View(this.User.Claims.ToDictionary(x => x.Type, x => x.Value));
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userRepository.Validate(model);
            if (user == null) return View(model);

            await _userManager.SignIn(this.HttpContext, user, isPersistent: false);

            //Cookie 
            CookieOptions unsecuredCookieOptions = new CookieOptions()
            {
                Secure = false,
                HttpOnly = false,
                IsEssential = true,
                Path = "/",
                Expires = DateTime.Now.AddMinutes(30),
                SameSite = SameSiteMode.Lax,
            };

            Response.Cookies.Append("email", user.EmailAddress, unsecuredCookieOptions);

            CookieOptions securedCookieOptions2 = new CookieOptions()
            {
                Secure = true,
                HttpOnly = true,
                IsEssential = true,
                Path = "/",
                Expires = DateTime.Now.AddMinutes(30),
                SameSite = SameSiteMode.Lax,
            };

            Response.Cookies.Append("user", user.Name, securedCookieOptions2);

            //HttpContext.Response.Cookie
            return LocalRedirect("~/Home/Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userRepository.Register(model);
            if(user == null)
            {
                return View(model);
            }
            await _userManager.SignIn(this.HttpContext, user, false);

            return LocalRedirect("~/Home/Index");
        }

        public async  Task<IActionResult> Logout()
        {
            await _userManager.SignOut(HttpContext);
            Response.Cookies.Delete("name");
            Response.Cookies.Delete("email");
            return RedirectPermanent("~/Home/Index");
        }

    }
}
