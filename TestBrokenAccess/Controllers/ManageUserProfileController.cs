using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestBrokenAccess.Models;
using TestBrokenAccess.Repositories;
using TestBrokenAccess.Validation;

namespace TestBrokenAccess.Controllers
{
    public class ManageUserProfileController : Controller
    {
        private IUserRepository _userRepository { get; set; }
        public ManageUserProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [Authorize]
        //[ValidateRole(RoleType.Admin)]
        public IActionResult Index()
        {
            List<CookieUserItem> users = _userRepository.GetListUsers();
            return View(users);
        }

        [HttpPost]
        public IActionResult UpdateRole(GrantRoleModel model)
        {


            return View("~/ManageUser");
        }
    }
}
