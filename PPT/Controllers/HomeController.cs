using FakeAPI.Controllers;
using FakeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using PPT.Lib;
using PPT.Shared.Models;

namespace PPT.Controllers
{
    public class HomeController : Controller
    {
        public UserModel CurrentUser { get; set; }

        public HomeController()
        {
            // Testcase: Je nach Wunsch auskommentieren
            //CurrentUser = UsersController.CreateSomeStaff();
            //CurrentUser = UsersController.CreateSomeAdmin();
            CurrentUser = UsersController.CreateSomeTrainer();
            //CurrentUser = UsersController.CreateSomeAttendee();
        }

        public IActionResult Index()
        {
            return View(CurrentUser);
        }

        // normalerweise wird ein action filter genutzt um die Permissions aus der Session auszulesen und den Benutzer zu autorisieren
        // dieser Actionfilter ist implementiert, da dies aber nur eine Prototyp ist, wird die Validierung im Methodenbody durchgeführt
        [RequirePermission(EPermissions.users_list_all)]
        public IActionResult Users()
        {
            if (CurrentUser.HasPermission(EPermissions.users_list_all))
            {
                // fake-login via direkter übergabe des CurrentUsers and die Methode
                var apiEndpoint = new UsersController();
                var benutzertabelle = apiEndpoint.GetCurrentUsers(CurrentUser);
                return View(benutzertabelle);
            } else // das else existiert später nur im ActionFilter
            {
                return View("Error");
            }
        }
    }
}
