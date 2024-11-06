using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class Product
    {
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int? CategoryId { get; set; }

        public double? Rating { get; set; }

        public string? ImageUrl { get; set; }

        public bool? Availability { get; set; }

        public virtual Category? Category { get; set; }

        public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

        public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

        public ICollection<Ingredient> ProductIngredients { get; set; } = new List<Ingredient>();

    }
}
