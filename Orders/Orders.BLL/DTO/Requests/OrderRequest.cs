using Orders.BLL.DTO.Responses;
using Orders.DAL.Models;

namespace Orders.BLL.DTO.Requests;

public class OrderRequest
{
    public int? UserId { get; set; }

    public string Status { get; set; } = null!;

    public string ShippingAddress { get; set; } = null!;

    public int? ProductId { get; set; }

    public int Quantity { get; set; }
    
    public int Price { get; set; }

}