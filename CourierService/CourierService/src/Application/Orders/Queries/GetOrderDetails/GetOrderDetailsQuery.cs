namespace CourierService.Application.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsQuery : IRequest<OrderDetailsDTO>
{
    public int Id { get; set; }
}
