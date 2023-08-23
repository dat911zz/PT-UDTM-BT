using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_T1
{
    public class FoodDB
    {
        public FoodDB()
        {

        }
        public List<string> getFoodList()
        {
            return new List<string>(){ "Grain", "Bread", "Beans", "Eggs", "Chicken", "Milk", "Fruit", "Vegetables", "Pasta", "Rice", "Fish", "Beef" };
        }
    }
}
