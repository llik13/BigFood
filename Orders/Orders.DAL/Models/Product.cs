﻿using System;
using System.Collections.Generic;

namespace Orders.DAL.Models;

public class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}