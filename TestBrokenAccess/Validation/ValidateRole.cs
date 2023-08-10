
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;
using System.Security.Claims;
//using System.Web.Mvc;
using TestBrokenAccess.Models;

namespace TestBrokenAccess.Validation
{
    public class ValidateRoleAttribute : ActionFilterAttribute
    {
        private RoleType _role;
        public ValidateRoleAttribute(RoleType role)
        {
            _role = role;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //base.OnActionExecuted(context);
            var user = context.HttpContext.User;

            if (user != null && user.Claims.Any(claim => claim.Type == ClaimTypes.Role))
            {
                if(!user.Claims.First(claim => claim.Type == ClaimTypes.Role).Value.Equals(_role.ToString()))
                {
                    context.Result = new RedirectResult("~/Unauthorization");
                }
            }
            else
            {
                context.Result = new RedirectResult("~/Unauthorization");
            }

            base.OnActionExecuted(context);
        }
    }
}
