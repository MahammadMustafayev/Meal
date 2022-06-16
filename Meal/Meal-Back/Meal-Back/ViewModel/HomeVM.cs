using Meal_Back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_Back.ViewModel
{
    public class HomeVM
    {
        public List<Menu> Menus { get; set; }
        public List<FoodBranch> FoodBranches { get; set; }
    }
}
