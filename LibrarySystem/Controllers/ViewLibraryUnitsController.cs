using LibrarySystem.Data;
using LibrarySystem.Models.Domain;
using LibrarySystem.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibrarySystem.Controllers
{
    public class ViewLibraryUnitsController : Controller
    {
        private readonly LibrarySystemDbContext _context;

        private List<string> sections;

        private IEnumerable<SelectListItem> SectionList;

        private List<string> types;

        private IEnumerable<SelectListItem> typeList;
        public ViewLibraryUnitsController(LibrarySystemDbContext _context)
        {

            this._context = _context;

            sections = new List<string>();

            foreach (var el in _context.Sections)
            {
                    sections.Add(el.Name);             
            }

            this.SectionList = sections.Select(x => new SelectListItem
            {
                Text = x.ToString()
            });           

           

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
        }


        [HttpGet]

        public IActionResult Index()
        {
            ViewBag.SectionList = this.SectionList;
            ViewBag.TypeList = this.typeList;
            var titlesList = _context.Titles.ToList();

            foreach(var el in _context.Titles.ToList())
            {
                if(!_context.LibraryUnits.Where(x=>x.Title==el).Any())
                {
                    titlesList.Remove(el);
                }
            }
            List<Title> titles = new List<Title>();
            
            if(NavigationHelper.navoptionList.Count>0)
            {
                
                if(NavigationHelper.navoptionList.Count==1) 
                {

                    if (sections.Contains(NavigationHelper.navoptionList[0]))
                    {
                        //section

                        titles = _context.Titles.Include(x => x.Section).Where(x => x.Section.Name == NavigationHelper.navoptionList[0]).ToList();
                        foreach (var el in _context.Titles.ToList())
                        {
                            if (!_context.LibraryUnits.Where(x => x.Title == el).Any())
                            {
                                titles.Remove(el);
                            }
                        }
                        if (titles == null || titles.Count == 0)
                        {
                            TempData["error"] = "Not Found!";
                            NavigationHelper.navoptionList.Clear();
                            return View(titlesList);
                        }
                        NavigationHelper.navoptionList.Clear();
                       
                        return View(titles);
                    }
                    else
                    {
                        //type

                        titles = _context.Titles.Where(x => x.Type == NavigationHelper.navoptionList[0]).ToList();
                        foreach (var el in _context.Titles.ToList())
                        {
                            if (!_context.LibraryUnits.Where(x => x.Title == el).Any())
                            {
                                titles.Remove(el);
                            }
                        }
                        if (titles == null || titles.Count==0)
                        {
                            TempData["error"] = "Not Found!";
                            NavigationHelper.navoptionList.Clear();
                            return View(titlesList);
                        }
                        NavigationHelper.navoptionList.Clear();
                       
                        return View(titles);

                    }
                }
                else
                {
                     titles= _context.Titles.Include(x=>x.Section).Where(x => x.Section.Name == NavigationHelper.navoptionList[0]).Where(x => x.Type == NavigationHelper.navoptionList[1]).ToList();


                    foreach (var el in _context.Titles.ToList())
                    {
                        if (!_context.LibraryUnits.Where(x => x.Title == el).Any())
                        {
                            titles.Remove(el);
                        }
                    }


                    if (titles==null || titles.Count == 0)
                    {
                        TempData["error"] = "Not Found!";
                        NavigationHelper.navoptionList.Clear();
                        return View(titlesList);
                    }
                    NavigationHelper.navoptionList.Clear();
                   
                    return View(titles);
                }
            }          
            else if (string.IsNullOrEmpty(NavigationHelper.searchString))
            {
                
                return View(titlesList);
            }
            else
            {

                if (NavigationHelper.searchoption == "bytitle")
                {
                    foreach (var el in titlesList)
                    {
                        string lowername = el.Name.ToLower();
                        if (lowername.Contains(NavigationHelper.searchString.ToLower()))
                        {                       
                            titles.Add(el);
                        }
                    }
                }
                else if(NavigationHelper.searchoption =="byauthor")
                {
                    foreach (var el in titlesList)
                    {
                        string lowername = el.Author.ToLower();
                        if (lowername.Contains(NavigationHelper.searchString.ToLower()))
                        {
                            titles.Add(el);
                        }
                    }
                }
                NavigationHelper.searchoption = string.Empty;
                NavigationHelper.searchString= string.Empty;
                if (titles.Count > 0) 
                {
                   
                    return View(titles);
                }
                else
                {
                    TempData["error"] = "Not Found!";
                    return View(titlesList);
                }
            }
        }

        [HttpPost]
        public IActionResult Index(string searchString)
        {
            NavigationHelper.searchoption = Request.Form["optionsRadios"];
            NavigationHelper.navoptionList.Clear();
            if (!string.IsNullOrEmpty(searchString))
            {
                NavigationHelper.searchString = searchString;
            }
            else
            {
                NavigationHelper.searchString = string.Empty;
                TempData["error"] = "Please, fill the search field!";
            }

            return RedirectToAction("Index", "ViewLibraryUnits");


        }

        [HttpPost]
        public IActionResult NavOptions(string section,string type)
        {    
            NavigationHelper.navoptionList.Clear();
            if(!string.IsNullOrEmpty(section))
            {
                NavigationHelper.navoptionList.Add(section);
            }

            if (!string.IsNullOrEmpty(type))
            {
                NavigationHelper.navoptionList.Add(type);
            }    
            

            if(NavigationHelper.navoptionList.Count==0) 
            {
                TempData["error"] = "Please, select at least one field!";
            }



            return RedirectToAction("Index", "ViewLibraryUnits");
        }

        [HttpGet]
        public IActionResult MakeRequest(Guid? id)
        {
            if (!string.IsNullOrEmpty(AccountManagment.Role) && AccountManagment.IsConfirmed == "Yes")
            {
                var libraryunit=_context.LibraryUnits.Include(x=>x.Title).Where(x=>x.Title.Id==id).FirstOrDefault();
                var movementlu = _context.Movements.Include(x=>x.Title).Where(x => x.Title.Id == id).FirstOrDefault();
                if(movementlu==null ||movementlu.Status=="Waiting")
                {
                    ViewBag.LuStatus = "Available";
                }
                else if(movementlu.Status=="Taken")
                {
                    //Available,Waiting,Taken

                    ViewBag.LuStatus = "Already Taken!";

                }

                return View(libraryunit);
            }
            else
            { return RedirectToAction("AccessDenied", "Home"); }
        }


        [HttpPost]
        public IActionResult MakeRequest(Guid titleid)
        {
            if (AccountManagment.Role=="Reader")
            {
                var user = _context.Users.FirstOrDefault(x => x.Nickname == AccountManagment.Nickname);
                var title = _context.Titles.FirstOrDefault(x => x.Id == titleid);
                if (!_context.Movements.Where(x => x.Reader == user).Where(x => x.Title == title).Any())
                {
                    MovementLibraryUnit movementLibraryUnit = new MovementLibraryUnit()
                    {
                        Title = title,
                        Reader = user,
                        Status = "Waiting",
                        Type = "Get",
                        Date = DateTime.UtcNow
                    };
                    _context.Movements.Add(movementLibraryUnit);
                    _context.SaveChanges();
                    TempData["success"] = "Your request is submitted successfully!";
                }
                else
                {
                    TempData["error"] = "You have already made a request for this library unit!";
                }

                return RedirectToAction("Index", "ViewLibraryUnits");
            }
            else
            {
                TempData["info"] = "Only readers can borrow library units :D";
                return RedirectToAction("Index", "ViewLibraryUnits");
            }
        }

    }

}
