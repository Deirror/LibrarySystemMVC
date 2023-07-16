using LibrarysSystem.Data;
using LibrarysSystem.Models.Domain;
using LibrarysSystem.Models.ViewModels;
using LibrarysSystem.User_Managment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Web.Helpers;

namespace LibrarysSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly LibrarysSystemDbContext _context;

        public LoginController(LibrarysSystemDbContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginRequest loginRequest)
        {
            // var verified = Crypto.VerifyHashedPassword(hash, password);
            //   Password = Crypto.HashPassword(loginRequest.Password),
            if (loginRequest != null)
            {
                var user = _context.Users.FirstOrDefault(x => x.Nickname == loginRequest.Nickname);
                if (user != null)
                {
                    var verified = Crypto.VerifyHashedPassword(user.Password, loginRequest.Password);

                    if (verified)
                    {
                        Account.Nickname = user.Nickname;
                        Account.IsLogged = true;
                        Account.Role = user.Role;

                        return RedirectToAction("Index", "Home");
          
                    }
                    else
                    {
     //remont
                        ModelState.AddModelError(nameof(loginRequest.Password), "Incorrect password!");
                     
                        return View(loginRequest);
                    }
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(nameof(loginRequest.Nickname), "User not found or matched!");
                    return View(loginRequest);
                }
            }
            else
            {
                ModelState.AddModelError(nameof(loginRequest.Nickname), "Please fill the field!");
                ModelState.AddModelError(nameof(loginRequest.Password), "Please fill the field!");
                return View("Login");
            }


           
        }
    }
}
