using Meal_Back.DAL;
using Meal_Back.Models;
using Meal_Back.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_Back.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class MenuController : Controller
    {
        private AppDbContext _context { get;  }
        private IWebHostEnvironment _env { get; }
        public MenuController(AppDbContext context,IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Menus.Include(x=>x.FoodBranch).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Menu menu)
        {
            if (menu.Photo.CheckSize(800))
            {
                ModelState.AddModelError("Photo", "File size must be less than 800kb");
                return View();
            }
            if (!menu.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "File must be image");
                return View();
            }
            menu.Image = await menu.Photo.SaveFileAsync(Path.Combine(_env.WebRootPath, "image"));
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));
            Menu menu = _context.Menus.Find(id);
            if (menu == null) return NotFound();
            return View(menu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Menu menu)
        {
            if (ModelState.IsValid)
            {
                var m = await _context.Menus.FindAsync(menu.Id);
                m.Title = menu.Title;
                m.Subtitle = menu.Subtitle;
                m.Price = menu.Price;
                if (menu.Photo != null)
                {
                    if (menu.Image != null)
                    {
                        string filePath = Path.Combine(_env.WebRootPath, "image", menu.Image);
                        System.IO.File.Delete(filePath);
                    }
                    m.Image = ProcessUploadedFile(menu);
                }
                _context.Update(m);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        private string ProcessUploadedFile(Menu menu)
        {
            string uniqueFileName = null;

            if (menu.Photo != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "image");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + menu.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    menu.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        public IActionResult Delete(int? id)
        {
            Menu menu = _context.Menus.Find(id);
            if (menu == null) return NotFound();
            _context.Menus.Remove(menu);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
