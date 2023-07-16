using LibrarysSystem.Data;
using LibrarysSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LibrarysSystem.Models.Domain;
using System.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using LibrarysSystem.User_Managment;
using NuGet.Protocol.Plugins;

namespace LibrarysSystem.Controllers
{
    
    public class RegisterController : Controller
    {
        private readonly LibrarysSystemDbContext _context;

        public RegisterController(LibrarysSystemDbContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            if (registerRequest != null)
            {
                try
                {
                    var user = new User()
                    {
                        Nickname = registerRequest.Nickname,
                        Password = Crypto.HashPassword(registerRequest.Password),
                        Role = "Reader",
                        Email = registerRequest.Email,
                        Name = registerRequest.Name
                    };

                    this._context.Users.Add(user);
                    this._context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch(Exception)
                {
                    ModelState.AddModelError(nameof(registerRequest.Nickname), "Please fill all the fields!");

                    return View("Register");
                }


               
            }
            else
            {
                return View("Register");
            }
        }

    }
}
