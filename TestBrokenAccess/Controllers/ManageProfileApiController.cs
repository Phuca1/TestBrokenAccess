using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestBrokenAccess.Models;
using TestBrokenAccess.Repositories;

namespace TestBrokenAccess.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageProfileApiController : ControllerBase
    {
        private IUserRepository _userRepository { get; set; }
        public ManageProfileApiController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Authorize]
        public string UpdateRole(GrantRoleModel model)
        {
            return "success";
        }

        //[Authorize]
        [HttpGet]
        public ActionResult GetProfile(string email)
        {

            if(email != null)
            {
                var user = _userRepository.GetUserInfo(email);

                //var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                return new ContentResult()
                {
                    Content = JsonConvert.SerializeObject(user),
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }

            return new ForbidResult();
        }
    }
}
