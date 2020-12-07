using System.Web.Mvc;

namespace CanTeenVLU.Areas.Admin.Middleware
{
    public class LoginVerification : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user-id"] == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Auth/Login");
                return;
            }
        }

    }
}