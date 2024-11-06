using System;
using System.Collections.Generic;

namespace Catalog.DataAccessLayer.Entities;

public partial class Product
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

    public virtual ICollection<Producttag> Producttags { get; set; } = new List<Producttag>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public ICollection<Ingredient> ProductIngredients { get; set; } = new List<Ingredient>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
