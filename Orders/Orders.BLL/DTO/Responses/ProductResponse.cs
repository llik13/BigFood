namespace Orders.BLL.DTO.Responses;

public class ProductResponse
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }
}