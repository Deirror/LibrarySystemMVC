using Microsoft.AspNetCore.Mvc;

namespace LibrarysSystem.Controllers
{
    public class AccessDeniedController : Controller
    {
        [HttpGet]
        public IActionResult AccessDeniedPage()
        {
            return View();
        }
    }
}
