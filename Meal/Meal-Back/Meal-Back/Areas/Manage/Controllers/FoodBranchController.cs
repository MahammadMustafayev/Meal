using Meal_Back.DAL;
using Meal_Back.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_Back.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class FoodBranchController : Controller
    {
        private readonly AppDbContext _context;

        public FoodBranchController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.FoodBranches.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FoodBranch food)
        {
            if (food == null) return NotFound();
            _context.FoodBranches.Add(food);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));
            FoodBranch food = _context.FoodBranches.FirstOrDefault(x => x.Id == id);
            if (food == null) return BadRequest();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FoodBranch foodBranch)
        {
            FoodBranch exstfood = _context.FoodBranches.FirstOrDefault(x => x.Id == foodBranch.Id);
            if (exstfood == null) return BadRequest();
            exstfood.BranchName = foodBranch.BranchName;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));
            FoodBranch food = _context.FoodBranches.Find(id);
            if (food == null) return NotFound();
            _context.FoodBranches.Remove(food);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
