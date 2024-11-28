using Orders.BLL.Enums;
using Orders.DAL.Models;

namespace Orders.BLL.DTO.Responses;

public class OrderResponse
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public OrderStatus Status { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public IEnumerable<OrderDetailResponse> OrderDetails { get; set; }

    public UserResponse? User { get; set; }
    
    //public String FullName {get; set;}
    //public String Email {get; set;}
    //public String Phone {get; set;}
}