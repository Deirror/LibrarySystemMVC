using LibrarysSystem.Data;
using LibrarysSystem.Models.Domain;
using LibrarysSystem.Models.ViewModels;
using LibrarysSystem.User_Managment;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;

namespace LibrarysSystem.Controllers
{
    public class CreateLibrarianController : Controller
    {
        private readonly LibrarysSystemDbContext _context;

        public CreateLibrarianController(LibrarysSystemDbContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (Account.Role=="Admin")
            {
                return View(); 
            }
            else
            {
                return RedirectToAction("AccessDeniedPage","AccessDenied");
            }
        }
        [HttpPost]
        public IActionResult Create(RegisterRequest registerRequest)
        {
            if (registerRequest != null)
            {
                var user = new User()
                {
                    Nickname = registerRequest.Nickname,
                    Password = Crypto.HashPassword(registerRequest.Password),
                    Role = "Librarian",
                    Email = registerRequest.Email,
                    Name = registerRequest.Name
                };

                this._context.Users.Add(user);
                this._context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Create");
            }
        }
    }
}
