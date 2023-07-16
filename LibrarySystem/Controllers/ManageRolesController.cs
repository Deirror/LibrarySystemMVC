using LibrarySystem.Data;
using LibrarySystem.Models.Domain;
using LibrarySystem.Models.ViewModels;
using LibrarySystem.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
using System.Web.Helpers;

namespace LibrarySystem.Controllers
{

    public class ManageRolesController : Controller
    {
        private readonly LibrarySystemDbContext _context;

        private List<string> answers;

        private IEnumerable<SelectListItem> answerList;
        public ManageRolesController(LibrarySystemDbContext _context)
        {
            this.answers = new List<string>();
            answers.Add("Yes");
            answers.Add("No");
            this.answerList = answers.Select(x => new SelectListItem
            {
                Text = x.ToString(),
            });

            this._context = _context;

        }



        [HttpGet]
        public IActionResult Index(string role)
        {

            if (AccountManagment.IsAdmin())
            {
                if (role != null) NavigationHelper.roleName = role;

                var accounts = _context.Users.Where(x => x.Role == NavigationHelper.roleName).ToList();

                return View(accounts);
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
        }

        [HttpGet]
        public IActionResult Upsert(Guid? id)
        {
            if (AccountManagment.IsAdmin())
            {
                TempData["success"] = null;

                ViewBag.AnswerList = this.answerList;


                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    //Edit
                    var user = _context.Users.FirstOrDefault(x => x.Id == id);
                    ViewBag.Action = "Edit";
                    List<string> roles = new List<string>() { "Reader", "Librarian", "Admin" };
                    IEnumerable<SelectListItem> RoleList = roles.Select(x => new SelectListItem
                    {
                        Text = x.ToString(),
                    });



                    ViewBag.RolesList = RoleList;
                    ViewBag.AnswerList = this.answerList;

                    return View(user);
                }
                else
                {
                    ViewBag.Action = "Create";
                    ViewBag.AnswerList = this.answerList;

                    return View();
                }
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
        }
    

    

        [HttpPost]
        public IActionResult Upsert(User obj)
        {
            if (NavigationHelper.roleName != "Reader")
            {
                obj.IsConfirmed = "Yes";
            }

            if (ModelState.IsValid)
            {
                if (_context.Users.Where(x => x.Id == obj.Id).Contains(obj))
                {

                    _context.Users.Update(obj);
                    _context.SaveChanges();
                    TempData["success"] = "Account updated successfully!";
                 
                    return RedirectToAction("Index", "ManageRoles");
                }
                else
                {
                    try
                    {

                        var user = new User()
                        {
                            Nickname = obj.Nickname,
                            Password = Crypto.HashPassword(obj.Password),
                            Role = NavigationHelper.roleName,
                            Email = obj.Email,
                            Name = obj.Name,
                            IsConfirmed = obj.IsConfirmed
                        };


                        _context.Users.Add(user);
                        _context.SaveChanges();
                        TempData["success"] = "Account created successfully!";

                        return RedirectToAction("Index", "ManageRoles");
                    }
                    catch(Exception )
                    {
                        if (_context.Users.Where(x => x.Email == obj.Email).Any() && _context.Users.Where(x => x.Nickname == obj.Nickname).Any())
                        {
                            ModelState.AddModelError(nameof(obj.Nickname), "Already exists!");
                            ModelState.AddModelError(nameof(obj.Email), "Already exists!");
                        }
                        else if (_context.Users.Where(x => x.Email == obj.Email).Any())
                        {
                            ModelState.AddModelError(nameof(obj.Email), "Already exists!");
                        }
                        else if (_context.Users.Where(x => x.Nickname == obj.Nickname).Any())
                        {
                            ModelState.AddModelError(nameof(obj.Nickname), "Already exists!");
                        }

                        ViewBag.AnswerList = this.answerList;
                        return View();
                    }
                }
            }
            else
            {
                return View("Index");
            }


        }

        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            if (AccountManagment.IsAdmin())
            {
                TempData["success"] = null;
            var account = _context.Users.FirstOrDefault(x => x.Id == id);
            return View(account);
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
        }

        [HttpPost]
        public IActionResult Delete(User user)
        {
            this._context.Users.Remove(user);
            this._context.SaveChanges();
            TempData["success"] = "Account deleted successfully!";

            return RedirectToAction("Index", "ManageRoles");
        }

        [HttpGet]
        public IActionResult Confirm(Guid id)
        {
            if (AccountManagment.IsAdmin())
            {
                var account = _context.Users.FirstOrDefault(x => x.Id == id);
                if (account != null)
                    account.IsConfirmed = "Yes";
                _context.SaveChanges();
                TempData["success"] = "Account confirmed successfully";
                return RedirectToAction("Index", "ManageRoles");
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
            }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (AccountManagment.IsAdmin())
            {

                return View();
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
        }
    

        [HttpPost]
        public IActionResult ChangePassword(PasswordRequest passwordRequest)
        {
            if (ModelState.IsValid)
            {
                bool flag = false;
                foreach (var el in _context.Users)
                {
                    if(Crypto.VerifyHashedPassword(el.Password, passwordRequest.OldPassword))
                    {
                        flag = true;
                        el.Password = Crypto.HashPassword(passwordRequest.NewPassword);
                        break;
                    }
                }
           
                    if (flag)
                    {                      
                        _context.SaveChanges();
                        TempData["success"] = "Successfully changed password!";
                        return RedirectToAction("Index", "ManageRoles");
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(passwordRequest.OldPassword), "Incorrect password!");
                        return View();
                    }
                    
            }
            else
            {
                return View();
            }
        }
    }
}
