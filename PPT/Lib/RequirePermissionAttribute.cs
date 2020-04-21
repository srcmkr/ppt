using Microsoft.AspNetCore.Mvc.Filters;
using PPT.Controllers;
using PPT.Shared.Models;
using System.Web.Http.Controllers;

namespace PPT.Lib
{
    public class RequirePermissionAttribute : ActionFilterAttribute
    {
        private readonly EPermissions reqPerm;

        // über den contructor wird die required permission an das funktionsattribut übergeben
        public RequirePermissionAttribute(EPermissions perm)
        {
            reqPerm = perm;
        }

        public void OnActionExecuting(HttpActionContext context)
        {
            // super contructor
            //base.OnActionExecuting(context);

            // read currentuser from parental controller
            var parentController = (HomeController)context.ControllerContext.Controller;
            var currentUser = parentController.CurrentUser;

            // prüfe auf permission:
            if (!currentUser.HasPermission(reqPerm))
            {
                context.Response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
            }
        }
    }
}
