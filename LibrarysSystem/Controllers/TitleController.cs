using LibrarysSystem.Data;
using LibrarysSystem.Models.Domain;
using LibrarysSystem.Models.ViewModels;
using LibrarysSystem.User_Managment;
using Microsoft.AspNetCore.Mvc;
using LibrarysSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace LibrarysSystem.Controllers
{
    public class TitleController : Controller
    {
        private readonly LibrarysSystemDbContext _context;
        private readonly IWebHostEnvironment _iweb;
        public TitleController(LibrarysSystemDbContext _context, IWebHostEnvironment iweb)
        {
            this._context = _context;
            _iweb = iweb;
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (Account.Role == "Admin" || Account.Role == "Librarian")
            {
             
                return View();
            }
            else
            {
                return RedirectToAction("AccessDeniedPage", "AccessDenied");
            }
        }
        [HttpPost]
        public IActionResult Create(TitleRequest titleRequest)
        {
           
                if (titleRequest != null)
                {
               
                    string ext = Path.GetExtension(TitleRequest.ImageFile.FileName);
                    if (ext == ".jpg")
                    {
                    string wwwRothPath = _iweb.WebRootPath;
                    titleRequest.ImageName = Path.GetFileName(TitleRequest.ImageFile.FileName);
                    string path = Path.Combine(wwwRothPath+"/Images/",titleRequest.ImageName);   

                    using(var fileStream = new FileStream(path,FileMode.Create))
                    {
                         TitleRequest.ImageFile.CopyToAsync(fileStream);
                    }
                       var section = _context.Sections.FirstOrDefault(s => s.Name == titleRequest.sectionName);
                  
                    
                        if (section != null)
                        {
                            var title = new Title()
                            {
                                Name = titleRequest.Name,
                                Description = titleRequest.Description,
                                Author = titleRequest.Author,
                                Year = titleRequest.Year,
                                ISBN = titleRequest.ISBN,
                                ImageUrl = "~/Images/" + titleRequest.ImageName,
                                Type = titleRequest.Type,
                                Publisher = titleRequest.Publisher,
                                PublishedYear = titleRequest.PublishedYear,
                                Section = section,
                            };
                            this._context.Titles.Add(title);
                            this._context.SaveChanges();

                        }
                    

                    }
                }
            

            return RedirectToAction("Details","Title");
        }


        [HttpGet]
        public IActionResult Edit(Guid id)
        {

            //var title = this._context.Titles.FirstOrDefault(x => x.Id == id);
            var title = this._context.Titles.Where(t=>t.Id == id).Include(t => t.Section).FirstOrDefault();

            if (title != null)
            {
                //var section = this._context.Sections.FirstOrDefault(x => x.Id == title.Section.Id);
                //title.sectionName = section.Name;
                
                return View(title);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Edit(Title Updatedtitle)
        {
            if (Updatedtitle != null)
            {
                var title = this._context.Titles.FirstOrDefault(x => x.Id == Updatedtitle.Id);
                if (title != null)
                {
                    
                        title.Name = Updatedtitle.Name;
                        title.Author = Updatedtitle.Author;
                        title.Description = Updatedtitle.Description;
                    title.Year = Updatedtitle.Year;
                    title.ISBN = Updatedtitle.ISBN;
                    title.Type = Updatedtitle.Type;
                    title.ImageUrl = Updatedtitle.ImageUrl;
                    title.PublishedYear= Updatedtitle.PublishedYear;
                    title.Publisher= Updatedtitle.Publisher;
                    title.Section = Updatedtitle.Section;

                        this._context.SaveChanges();
                        return RedirectToAction("Details", "Title");
                    
                }
            }
            //remont za nullable
            return RedirectToAction("Edit", "Title");
        }
        [HttpGet]
        public IActionResult Details()
        {
            List<Title> titles = _context.Titles.ToList();
            return View(titles);
        }

    
    }
}
