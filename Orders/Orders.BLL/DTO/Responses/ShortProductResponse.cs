namespace Orders.BLL.DTO.Responses;

public class ShortProductResponse
{
    
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;
    
    public decimal Price { get; set; }
}