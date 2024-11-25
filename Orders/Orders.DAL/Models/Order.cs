using System;
using System.Collections.Generic;

namespace Orders.DAL.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime OrderDate { get; set; }
    
    public string Status { get; set; } = null!;

    public string ShippingAddress { get; set; } = null!;

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal? LineTotal { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
