using System;
using System.Collections.Generic;
using System.Net.Mail;
using Orders.BLL.Enums;

namespace Orders.DAL.Models;

public class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }
    public string? DeliverId { get; set; }
    public string Number { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public virtual IEnumerable<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}
