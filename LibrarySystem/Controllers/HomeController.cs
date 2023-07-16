using LibrarySystem.Data;
using LibrarySystem.Models;
using LibrarySystem.Models.Domain;
using LibrarySystem.Models.ViewModels;
using LibrarySystem.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.Helpers;

namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LibrarySystemDbContext _context;

        public HomeController(ILogger<HomeController> logger, LibrarySystemDbContext _context)
        {
            this._context = _context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            if (string.IsNullOrEmpty(AccountManagment.Role))
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginRequest loginRequest)
        {
            
            // var verified = Crypto.VerifyHashedPassword(hash, password);
            // Password = Crypto.HashPassword(loginRequest.Password),
            if (loginRequest != null)
            {
                var user = _context.Users.FirstOrDefault(x => x.Nickname == loginRequest.Nickname);
                if (user != null)
                {
                    var verified = Crypto.VerifyHashedPassword(user.Password, loginRequest.Password);

                    if (verified)
                    {
                        AccountManagment.Nickname = user.Nickname;
                        AccountManagment.Role = user.Role;
                        AccountManagment.IsConfirmed = user.IsConfirmed;

                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ModelState.AddModelError(nameof(loginRequest.Password), "Incorrect password!");

                        return View(loginRequest);
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(loginRequest.Nickname), "Incorrect nickname!");
                    return View(loginRequest);
                }
            }
            else
            {

                return View("Login");
            }


        }

        #endregion

        #region Logout
        [HttpGet]
        public IActionResult Logout()
        {
            if (!string.IsNullOrEmpty(AccountManagment.Role))
            {
                AccountManagment.Nickname = null;
                AccountManagment.Role = null;
                return RedirectToAction("Index", "Home");
            }
            else return RedirectToAction("AccessDenied", "Home");
        }
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            if (string.IsNullOrEmpty(AccountManagment.Role))
            {
                return View();
            }      
            else return RedirectToAction("AccessDenied","Home");

    }

        [HttpPost]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User()
                    {
                        Nickname = registerRequest.Nickname,
                        Password = Crypto.HashPassword(registerRequest.Password),
                        Role = "Reader",
                        Email = registerRequest.Email,
                        Name = registerRequest.Name,
                        IsConfirmed = "No"
                    };
                    AccountManagment.Role = "Reader";
                    AccountManagment.Nickname = user.Nickname;
                    AccountManagment.IsConfirmed = "No";

                   

                    _context.Users.Add(user);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {
                    if (_context.Users.Where(x => x.Email == registerRequest.Email).Any()&&_context.Users.Where(x => x.Nickname == registerRequest.Nickname).Any())
                    {
                        ModelState.AddModelError(nameof(registerRequest.Nickname), "Already exists!");
                        ModelState.AddModelError(nameof(registerRequest.Email), "Already exists!");
                    }
                    else  if (_context.Users.Where(x => x.Email == registerRequest.Email).Any())
                    {
                        ModelState.AddModelError(nameof(registerRequest.Email), "Already exists!");
                    }
                    else if(_context.Users.Where(x => x.Nickname == registerRequest.Nickname).Any())
                    {
                        ModelState.AddModelError(nameof(registerRequest.Nickname), "Already exists!");
                    }

                    return View();
                }

            }
            else
            {
                return View("Register");
            }


        }

        #endregion

        #region Profile

        [HttpGet]
        public IActionResult Profile()
        {
            if (!string.IsNullOrEmpty(AccountManagment.Role))
            {
                var user = _context.Users.FirstOrDefault(x => x.Nickname == AccountManagment.Nickname);
                return View(user);
            }
            else return RedirectToAction("AccessDenied","Home");

        }
        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}