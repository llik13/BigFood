namespace Orders.BLL.DTO.Responses;

public class ShortOrderResponse
{
    
    public int Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public string Status { get; set; } = null!;

    public int? ProductId { get; set; }

    public decimal? LineTotal { get; set; }
    
    public virtual IEnumerable<OrderDetailResponse>? OrderDetail { get; set; }

}