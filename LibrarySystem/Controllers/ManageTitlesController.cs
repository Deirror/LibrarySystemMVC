using LibrarySystem.Data;
using LibrarySystem.Models.Domain;
using LibrarySystem.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;

namespace LibrarySystem.Controllers
{
    
    public class ManageTitlesController : Controller
    {
        private readonly LibrarySystemDbContext _context;
        private readonly IWebHostEnvironment _iweb;

        private List<string> types;

        private IEnumerable<SelectListItem> typeList;

        private List<string> sectionnames;

        private IEnumerable<SelectListItem> sectionameList;
        public ManageTitlesController(LibrarySystemDbContext _context, IWebHostEnvironment iweb)
        {
            this._context = _context;
            _iweb = iweb;

            this.types = new List<string>();
            types.Add("Art Book");
            types.Add("Magazine");
            types.Add("Textbook");
            types.Add("Notes");
            types.Add("Film");
            this.typeList = types.Select(x => new SelectListItem
            {
                Text = x.ToString(),
            });


            this.sectionnames = new List<string>();
            foreach(var el in _context.Sections)sectionnames.Add(el.Name);

            this.sectionameList = sectionnames.Select(x => new SelectListItem
            {
                Text = x.ToString(),
            });

        }
        public IActionResult Index()
        {
            if (AccountManagment.IsAdmin() || AccountManagment.IsLibrarian())
            {
                var titles = _context.Titles.ToList();

                return View(titles);
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
                    var title = _context.Titles.FirstOrDefault(x => x.Id == id);
                    ViewBag.Action = "Edit";
                    ViewBag.TypeList = typeList;
                    ViewBag.SectionNameList = sectionameList;
                    return View(title);
                }
                else
                {
                    //Create
                    ViewBag.Action = "Create";
                    ViewBag.TypeList = typeList;
                    ViewBag.SectionNameList = sectionameList;
                    return View();
                }
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
        }

        [HttpPost]
        public IActionResult Upsert(Title obj,IFormFile? file,string? sectionName)
        {

            if (ModelState.IsValid)
            {
                if (_context.Titles.Where(x => x.Id == obj.Id).Contains(obj))
                {
                    //Edit


                    if (file != null)
                    {
                    
                        string wwwRothPath = _iweb.WebRootPath;

                        string fileName = Path.GetFileName(file.FileName);
                        string path = Path.Combine(wwwRothPath ,@"Images");

                        if (!string.IsNullOrEmpty(obj.ImageUrl))
                        {
                            var oldImage = Path.Combine(wwwRothPath, obj.ImageUrl.TrimStart('\\'));

                            if (System.IO.File.Exists(oldImage))
                            {
                                System.IO.File.Delete(oldImage);
                            }
                        }


                        using (var fileStream = new FileStream(Path.Combine(path,fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        
                      
                        obj.ImageUrl = @"\Images\" + fileName;
                        
                    }

                    if (!string.IsNullOrEmpty(sectionName))
                    {
                        var section = _context.Sections.FirstOrDefault(s => s.Name == sectionName);
                        obj.Section = section;
                    }

                        _context.Titles.Update(obj);
                        _context.SaveChanges();
                        TempData["success"] = "Title updated successfully!";
                    

                    return RedirectToAction("Index", "ManageTitles");
                }
                else
                {
                    //Create
                    try
                    {
                        string wwwRothPath = _iweb.WebRootPath;
                        if (file != null)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            string path = Path.Combine(wwwRothPath, @"Images");


                            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }

                            var section = _context.Sections.FirstOrDefault(s => s.Name == sectionName);

                            obj.ImageUrl = @"\Images\" + fileName;
                            obj.Section = section;

                            _context.Titles.Add(obj);
                            _context.SaveChanges();
                            TempData["success"] = "Title created successfully!";
                            return RedirectToAction("Index", "ManageTitles");
                        }
                        else
                        {
                            ModelState.AddModelError(nameof(obj.ImageUrl), "Please, choose an image!");
                            ViewBag.Action = "Create";
                            ViewBag.TypeList = typeList;
                            ViewBag.SectionNameList = sectionameList;
                            return View();
                        }
                    }
                    catch(Microsoft.EntityFrameworkCore.DbUpdateException)
                    {

                        if (obj.Section == null)
                        {
                            ModelState.AddModelError(nameof(obj.Section), "Choose section!");
                            ViewBag.Action = "Create";
                            ViewBag.TypeList = typeList;
                            ViewBag.SectionNameList = sectionameList;
   
                        }
                       
                        if(_context.Titles.Where(x=>x.ImageUrl==obj.ImageUrl).Any())
                        {
                            ModelState.AddModelError(nameof(obj.ImageUrl), "Already exists!");
                            ViewBag.Action = "Create";
                            ViewBag.TypeList = typeList;
                            ViewBag.SectionNameList = sectionameList;

                        }

                        return View();

                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(nameof(obj.Name), "Already exists!");
                        ViewBag.Action = "Create";
                        ViewBag.TypeList = typeList;
                        ViewBag.SectionNameList = sectionameList;
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
        public IActionResult Delete(Guid id)
        {
            if (AccountManagment.IsAdmin() || AccountManagment.IsLibrarian())
            {
                TempData["success"] = null;
                var title = _context.Titles.Find(id);
                return View(title);
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
            }
        [HttpPost]
        public IActionResult Delete(Title obj)
        {

            if (!string.IsNullOrEmpty(obj.ImageUrl))
            {
                var oldImage = Path.Combine(_iweb.WebRootPath, obj.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }
            }

            _context.Titles.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Title deleted successfully!";
            return RedirectToAction("Index");
        }

    }
}
