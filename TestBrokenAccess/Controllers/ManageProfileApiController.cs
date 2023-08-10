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

        [Authorize]
        public ActionResult GetProfile(string id)
        {

            if(Guid.TryParse(id, out var profileId))
            {
                var user = _userRepository.GetUserInfo(profileId);

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
