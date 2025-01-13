using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FribergCarRental.Filters
{
    public class SimpleAuthorizeAttribute : ActionFilterAttribute
    {
        public string Role { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.Session.GetString("User");
            var role = context.HttpContext.Session.GetString("Role");
            if (user == null)
            {
                context.Result = new RedirectResult("~/Account/Login");
            }
            if (Role != null && !Role.Equals(role))
            {
                context.Result = new RedirectResult("~/Account/AccessDenied");
            }
        }
    }
}
