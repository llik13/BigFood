namespace Orders.BLL.DTO.Responses;

public class OrderDetailResponse
{
    
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal? LineTotal { get; set; }

    public ProductResponse? Product { get; set; }
}