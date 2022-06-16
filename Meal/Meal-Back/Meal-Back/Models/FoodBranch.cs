using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meal_Back.Models
{
    public class FoodBranch
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
