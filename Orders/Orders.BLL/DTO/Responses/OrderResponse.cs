using Orders.DAL.Models;

namespace Orders.BLL.DTO.Responses;

public class OrderResponse
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public string ShippingAddress { get; set; } = null!;

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal? LineTotal { get; set; }

    public virtual ShortProductResponse? Product { get; set; }

    public virtual UserResponse? User { get; set; }
    
    //public String FullName {get; set;}
    //public String Email {get; set;}
    //public String Phone {get; set;}
}