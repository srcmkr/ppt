using FakeAPI.Controllers;
using FakeAPI.Models;
using Newtonsoft.Json;
using PPT.Shared.Models;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FakeAPI.Lib
{
    public class RequirePermissionAttribute : ActionFilterAttribute
    {
        private readonly EPermissions reqPerm;

        // über den contructor wird die required permission an das funktionsattribut übergeben
        public RequirePermissionAttribute(EPermissions perm)
        {
            reqPerm = perm;
        }

        // diese funktion wird ausgeführt bevor der eigentliche methodenrumpf ausgeführt wird
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // super contructor
            base.OnActionExecuting(actionContext);

            // erhalte den token aus dem request und lade entsprechende daten aus datenbank
            // einfach passende Rolle auskommentieren:
            //var currentUser = UsersController.CreateSomeStaff();
            //var currentUser = UsersController.CreateSomeAdmin();
            var currentUser = UsersController.CreateSomeAttendee();

            // wenn der Benutzer nicht die permission hat, sofortiger Abbruch z.B. via ApiResponse
            if (!currentUser.HasPermission(reqPerm))
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new ApiResponse
                    {
                        Success = false,
                        ErrorCode = "123",
                        Message = "Authorization failure"
                    }), Encoding.UTF8, "application/json")
                });
            }
            // else: nichts - der methodenrumpf wird einfach ausgeführt, wenn hier keine exception kommt
        }
    }
}