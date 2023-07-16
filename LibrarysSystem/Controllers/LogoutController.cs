using LibrarysSystem.User_Managment;
using Microsoft.AspNetCore.Mvc;

namespace LibrarysSystem.Controllers
{
    public class LogoutController : Controller
    {
        [HttpGet]
        public IActionResult Logout()
        {
            Account.IsLogged = false;
            Account.Nickname = string.Empty;
            Account.Role= string.Empty;
            return RedirectToAction("Index", "Home");
        }
    }
}
