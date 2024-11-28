using Orders.BLL.DTO.Responses;
using Orders.BLL.Enums;
using Orders.DAL.Models;

namespace Orders.BLL.DTO.Requests;

public class OrderRequest
{

    public int Id { get; set; }

    public int? UserId { get; set; }

    public int Status { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public IEnumerable<OrderDetailRequest> OrderDetails { get; set; }
    
}