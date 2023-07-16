using LibrarySystem.Data;
using LibrarySystem.Models.Domain;
using LibrarySystem.Models.ViewModels;
using LibrarySystem.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Web.Helpers;

namespace LibrarySystem.Controllers
{
    public class ManageSectionsController : Controller
    {
        private readonly LibrarySystemDbContext _context;

        public ManageSectionsController(LibrarySystemDbContext _context)
        {
            this._context = _context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            if (AccountManagment.IsAdmin() || AccountManagment.IsLibrarian())
            {
                var sections = _context.Sections.ToList();

                return View(sections);
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
        }



        [HttpGet]
        public IActionResult Upsert(Guid? id)
        {
            if (AccountManagment.IsAdmin() || AccountManagment.IsLibrarian())
            {

                TempData["success"] = null;


            if (!string.IsNullOrEmpty(id.ToString()))
            {
                //Edit
                var section = _context.Sections.FirstOrDefault(x => x.Id == id);
                ViewBag.Action = "Edit";

                return View(section);
            }
            else
            {
                ViewBag.Action = "Create";

                return View();
            }
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
        }

        [HttpPost]
        public IActionResult Upsert(Section obj)
        {

            if (ModelState.IsValid)
            {
                if (_context.Sections.Where(x => x.Id == obj.Id).Contains(obj))
                {
                    _context.Sections.Update(obj);
                    _context.SaveChanges();
                    TempData["success"] = "Section updated successfully!";

                    return RedirectToAction("Index", "ManageSections");
                }
                else
                {
                    try
                    {
                        _context.Sections.Add(obj);
                        _context.SaveChanges();
                        TempData["success"] = "Section created successfully!";
                        return RedirectToAction("Index", "ManageSections");
                    }
                    catch(Exception)
                    {
                        ModelState.AddModelError(nameof(obj.Name), "Already exists!");
                        return View();
                    }
                    
                }
            }
            else
            {
                return View();
            }


        }

        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            if (AccountManagment.IsAdmin() || AccountManagment.IsLibrarian())
            {
                TempData["success"] = null;
            var section = _context.Sections.FirstOrDefault(x => x.Id == id);
            return View(section);
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
        }

        [HttpPost]
        public IActionResult Delete(Section section)
        {
            this._context.Sections.Remove(section);
            this._context.SaveChanges();
            TempData["success"] = "Section deleted successfully!";
            return RedirectToAction("Index", "ManageSections");
        }

    }
}
