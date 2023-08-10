using Microsoft.AspNetCore.Mvc;

namespace TestBrokenAccess.Controllers
{
    public class UnauthorizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
