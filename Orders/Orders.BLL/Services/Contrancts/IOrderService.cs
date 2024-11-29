using Orders.BLL.DTO.Requests;
using Orders.BLL.DTO.Responses;
using Orders.BLL.Enums;
using Orders.DAL.Models;
using Orders.DAL.Pagination.Parameters;
using Orders.DAL.Specification;

namespace Orders.BLL.Services.Contrancts;

public interface IOrderService
{
    Task<IEnumerable<ShortOrderResponse>> GetAllOrdersAsync(OrderParameters orderParameters
        //ISpecification<Order> specification
        );
    Task<OrderResponse> GetOrderByIdAsync(int orderId);
    Task<OrderResponse> AddOrderAsync(OrderRequest order);
    Task<OrderResponse> UpdateOrderAsync(OrderRequest order);
    Task DeleteOrderAsync(int id);
    Task ChangeStatus(int id, OrderStatus newStatus);
}