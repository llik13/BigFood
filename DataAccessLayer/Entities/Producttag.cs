using System;
using System.Collections.Generic;

namespace Catalog.DataAccessLayer.Entities;

public partial class Producttag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}
