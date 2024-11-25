namespace CourierService.Application.Orders.Queries.GetOrderDetails;

public class OrderDetailsDTO
{
    public int Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public string ShippingAddress { get; set; } = null!;

    public int Quantity { get; set; }
    
    public decimal? LineTotal { get; set; }
    
    // info about product
    public int? ProductId { get; set; }

    public string ProductName { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    // info about user
    public int? UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

}
