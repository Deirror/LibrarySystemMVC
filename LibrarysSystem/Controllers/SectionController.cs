using LibrarysSystem.Data;
using LibrarysSystem.Models.Domain;
using LibrarysSystem.Models.ViewModels;
using LibrarysSystem.User_Managment;
using Microsoft.AspNetCore.Mvc;

namespace LibrarysSystem.Controllers
{
    public class SectionController : Controller
    {
        private readonly LibrarysSystemDbContext _context;

        public SectionController(LibrarysSystemDbContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (Account.Role == "Admin" || Account.Role=="Librarian")
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccessDeniedPage", "AccessDenied");
            }
        }

        [HttpPost]
        public IActionResult Create(SectionRequest sectionRequest)
        {
            if(sectionRequest != null)
            {
                var section = new Section()
                {
                    Name = sectionRequest.Name,
                    Description = sectionRequest.Description
                };
                _context.Sections.Add(section);
                _context.SaveChanges();
            }
            //REMONT ZA greshka
            return RedirectToAction("Details", "Section");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
           
            var section = this._context.Sections.FirstOrDefault(x=>x.Id==id);

            if (section != null)
            {

                return View(section);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost]
        public IActionResult Edit(Section Updatedsection)
        {
            if (Updatedsection != null)
            {
                var section = this._context.Sections.FirstOrDefault(x => x.Id == Updatedsection.Id);
                if (section != null)
                {
                    if (!string.IsNullOrEmpty(Updatedsection.Name)&&!string.IsNullOrEmpty(Updatedsection.Description)) 
                    { 
                        section.Name= Updatedsection.Name;
                        section.Description= Updatedsection.Description;

                        this._context.SaveChanges();
                        return RedirectToAction("Details", "Section");
                    }
                }
            }
//remont za nullable
            return RedirectToAction("Edit", "Section");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {

            var section = this._context.Sections.FirstOrDefault(x => x.Id == id);

            if (section != null)
            {
                
                return View(section);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Delete(Section section)
        {
            this._context.Sections.Remove(section);
            this._context.SaveChanges();
  
            return RedirectToAction("Details", "Section");
        }

        [HttpGet]
        public IActionResult Details()
        {
            List<Section> sections = _context.Sections.ToList();
            return View(sections);
        }
    }
}
