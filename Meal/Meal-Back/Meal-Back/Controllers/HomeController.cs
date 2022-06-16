using Meal_Back.DAL;
using Meal_Back.Models;
using Meal_Back.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_Back.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get;  }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Menus=_context.Menus.Take(5).ToList(),
                FoodBranches=_context.FoodBranches.ToList()
            };
            return View(homeVM);
        }

       
    }
}
