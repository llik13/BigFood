using System;
using System.Collections.Generic;

namespace Orders.DAL.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal? LineTotal { get; set; }

    public Order? Order { get; set; }

    public Product? Product { get; set; }
}
