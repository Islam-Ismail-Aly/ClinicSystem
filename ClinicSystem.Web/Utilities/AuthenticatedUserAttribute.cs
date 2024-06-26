﻿
namespace ClinicSystem.Web.Utilities
{
    public class AuthenticatedUsersAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated &&
                (context.HttpContext.Request.Path.StartsWithSegments("/Account/Login") ||
                 context.HttpContext.Request.Path.StartsWithSegments("/Account/Register")))
            {
                context.Result = new RedirectResult("/Home/Index");
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
