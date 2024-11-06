using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }

        public ICollection<Product> ProductIngredients { get; set; } = new List<Product>();
    }
}
