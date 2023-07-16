using LibrarySystem.Data;
using LibrarySystem.Models.Domain;
using LibrarySystem.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
{
    
    public class ManageRequestsController : Controller
    {
        private readonly LibrarySystemDbContext _context;
        private List<string> titles;

        private IEnumerable<SelectListItem> TitleList;

        public ManageRequestsController(LibrarySystemDbContext _context)
        {
            this._context = _context;

            titles= new List<string>();

            foreach(var el in _context.Titles)
            {
                titles.Add(el.Name);
            }

            this.TitleList = titles.Select(x => new SelectListItem
            {
                Text = x.ToString()
            });

        }


        [HttpGet]
        public IActionResult IndexForReaders()
        {
            if (AccountManagment.IsReader() && AccountManagment.IsConfirmed=="Yes")
            {
                var reader = _context.Users.Where(x => x.Nickname == AccountManagment.Nickname).FirstOrDefault();
                var libraryunits = _context.Movements.Include(x => x.Title).Include(x => x.Librarian).Where(x => x.Reader == reader).ToList();
                return View(libraryunits);
            }
            else { return RedirectToAction("AccessDenied","Home"); }
        }

        [HttpPost]
        public IActionResult IndexForReaders(Guid id)
        {
            var movlu = _context.Movements.Include(x => x.Librarian).Include(x => x.Reader).Include(x => x.Title).FirstOrDefault(x => x.Id == id);
            _context.Movements.Remove(movlu);
            _context.SaveChanges();
            TempData["success"] = "Successfully canceled order!";
            return RedirectToAction("IndexForReaders","ManageRequests");
        }

        [HttpGet]
        public IActionResult IndexForLibrarians()
        {
            ViewBag.TitleList = this.TitleList;
            var movements = _context.Movements.Include(x => x.Title).Include(x => x.Reader).Include(x => x.Librarian).ToList();
            if (AccountManagment.IsLibrarian()||AccountManagment.IsAdmin())
            {

                if (!string.IsNullOrEmpty(NavigationHelper.showoption))
                {

                    var title = _context.Titles.FirstOrDefault(x => x.Name == NavigationHelper.showoption);
                    var movementslu = _context.Movements.Include(x => x.Title).Include(x => x.Reader).Include(x => x.Librarian).Where(x => x.Title == title).ToList();
                    NavigationHelper.showoption = null;
                    return View(movementslu);
                }
                else
                {             
                 
                    return View(movements);
                }


            }
            else { return RedirectToAction("AccessDenied", "Home"); }
        }


        [HttpPost]
        public IActionResult IndexForLibrarians(string showoption)
        {
      
            if (string.IsNullOrEmpty(showoption))
            {              
                TempData["error"] = "Please, select a title!";
            }
        
                 NavigationHelper.showoption = showoption;
                return RedirectToAction("IndexForLibrarians", "ManageRequests");          
        }



        [HttpGet]

        public IActionResult Change(Guid id)
        {
            if (AccountManagment.IsLibrarian() || AccountManagment.IsAdmin())
            {

                var movementslu = _context.Movements.Include(x => x.Title).Include(x => x.Reader).Include(x => x.Librarian).Where(x => x.Id == id).FirstOrDefault();

                return View(movementslu);
            }
            else { return RedirectToAction("AccessDenied", "Home"); }
        }

        [HttpPost]
        public IActionResult Change(Guid id,DateTime? datetime)
        {
           if(datetime==null)
           {
                TempData["error"] = "Please, select date and time!";
                return RedirectToAction("Change", "ManageRequests");
           }
           else
           {
                var movlu = _context.Movements.Include(x => x.Librarian).Include(x => x.Reader).Include(x => x.Title).FirstOrDefault(x => x.Id == id);
                var librarian = _context.Users.Where(x => x.Nickname == AccountManagment.Nickname).FirstOrDefault();
                movlu.TimeLimit = datetime;
                movlu.Librarian = librarian;
                movlu.Status = "Taken";
                _context.Update(movlu);
                _context.SaveChanges();

                if (_context.Movements.Where(x => x.Title.Name == movlu.Title.Name).Any())
                {
                    foreach (var el in _context.Movements)
                    {
                        if (el.Status == "Waiting" && el.Title.Name == movlu.Title.Name)
                        {
                            _context.Remove(el);
                        }
                    }
                    _context.SaveChanges();

                }

                return RedirectToAction("IndexForLibrarians", "ManageRequests");
            }
  
          
        }

        [HttpGet]
        public IActionResult Cancel(Guid id)
        {
            if (AccountManagment.IsLibrarian() || AccountManagment.IsAdmin())
            {
                var movlu = _context.Movements.Include(x => x.Librarian).Include(x => x.Reader).Include(x => x.Title).FirstOrDefault(x => x.Id == id);
                _context.Remove(movlu);
                _context.SaveChanges();
                return RedirectToAction("IndexForLibrarians", "ManageRequests");
            }
            else { return RedirectToAction("AccessDenied", "Home"); }
        }


        [HttpGet]
        public IActionResult ChangeToReturn(Guid id)
        {
            if (AccountManagment.IsLibrarian() || AccountManagment.IsAdmin())
            {
                var movlu = _context.Movements.Include(x => x.Librarian).Include(x => x.Reader).Include(x => x.Title).FirstOrDefault(x => x.Id == id);
            movlu.Type = "Return";
            movlu.TimeLimit = null;
            _context.Update(movlu);
            _context.SaveChanges();
            return RedirectToAction("IndexForLibrarians", "ManageRequests");
            }
            else { return RedirectToAction("AccessDenied", "Home"); }
        }
    

        [HttpGet]
        public IActionResult CompleteRequest(Guid id)
        {
            if (AccountManagment.IsLibrarian() || AccountManagment.IsAdmin())
            {
                var movlu = _context.Movements.Include(x => x.Librarian).Include(x => x.Reader).Include(x => x.Title).FirstOrDefault(x => x.Id == id);
            _context.Remove(movlu);
            _context.SaveChanges();
            return RedirectToAction("IndexForLibrarians", "ManageRequests");
            }
            else { return RedirectToAction("AccessDenied", "Home"); }
        
        }
    }
}
