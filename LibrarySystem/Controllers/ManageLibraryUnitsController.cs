using LibrarySystem.Data;
using LibrarySystem.Models.Domain;
using LibrarySystem.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
{
    
    public class ManageLibraryUnitsController : Controller
    {
        private readonly LibrarySystemDbContext _context;

        private List<string> titles;

        private IEnumerable<SelectListItem> TitleList;

        private List<string> conditions;

        private IEnumerable<SelectListItem> ConditionList;
        private List<string> mediums;

        private IEnumerable<SelectListItem> MediumList;

        private List<string> places;

        private IEnumerable<SelectListItem> PlaceList;
        public ManageLibraryUnitsController(LibrarySystemDbContext _context)
        {
            this._context = _context;

            titles = new List<string>();
            var titlelist = this._context.Titles.ToList();
            foreach (var el in  titlelist)
            {
                if (!_context.LibraryUnits.Where(x => x.Title.Name == el.Name).Any())
                {
                    titles.Add(el.Name);
                }
            }

            this.TitleList = titles.Select(x=>new SelectListItem
            {
                Text = x.ToString()
            });

            conditions = new List<string>()
            {
                "Good","Damaged","Unsuitable"
            };
            this.ConditionList = conditions.Select(x => new SelectListItem
            {
                Text=x.ToString()
            });

            mediums = new List<string>()
            {
                "Paper","Disc","Tape Recorder"
            };
            this.MediumList = mediums.Select(x => new SelectListItem
            {
                Text = x.ToString()
            });

            places = new List<string>()
            {
                "Library Despot","Store Orange","Shop TechBuds"
            };
            this.PlaceList = places.Select(x => new SelectListItem
            {
                Text = x.ToString()
            });

        }

        public IActionResult Index()
        {
            if (AccountManagment.IsAdmin() || AccountManagment.IsLibrarian())
            {
                var libraryunits = _context.LibraryUnits.ToList();

                return View(libraryunits);
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
                    var libraryunit = _context.LibraryUnits.Include(x => x.Title).FirstOrDefault(x=>x.Id==id);
                    ViewBag.Action = "Edit";
                    ViewBag.TitleList = this.TitleList;
                    ViewBag.ConditionList = this.ConditionList;
                    ViewBag.MediumList=this.MediumList;
                    ViewBag.PlaceList = this.PlaceList;
                    return View(libraryunit);
                }
                else
                {
                    ViewBag.Action = "Create";
                    ViewBag.TitleList = this.TitleList;
                    ViewBag.ConditionList = this.ConditionList;
                    ViewBag.MediumList = this.MediumList;
                    ViewBag.PlaceList = this.PlaceList;
                    return View();
                }
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
        }



        [HttpPost]

        public IActionResult Upsert(LibraryUnit libraryUnit, string? titlename)
        {
            if (ModelState.IsValid)
            {
                if (_context.LibraryUnits.Where(x => x.Id == libraryUnit.Id).Contains(libraryUnit))
                {
                    if (titlename != null)
                    {
                        var title = _context.Titles.FirstOrDefault(x => x.Name == titlename);
                        libraryUnit.Title = title;
                    }
                    TempData["success"] = "Unit updated successfully!";
                    _context.LibraryUnits.Update(libraryUnit);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    if (titlename != null)
                    {
                        var title = _context.Titles.FirstOrDefault(x => x.Name == titlename);
                        libraryUnit.Title = title;
                        TempData["success"] = "Unit created successfully!";
                        _context.LibraryUnits.Add(libraryUnit);
                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "Title field must be populated!";
                        return RedirectToAction("Upsert");
                    }
                }
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            if (AccountManagment.IsAdmin() || AccountManagment.IsLibrarian())
            {
                TempData["success"] = null;
                var libraryunit = _context.LibraryUnits.Find(id);
                return View(libraryunit);
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
            }
        [HttpPost]
        public IActionResult Delete(LibraryUnit obj)
        {
          
            _context.LibraryUnits.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Unit deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
