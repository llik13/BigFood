using System.Diagnostics.Eventing.Reader;
using AutoMapper;
using Orders.BLL.DTO.Requests;
using Orders.BLL.DTO.Responses;
using Orders.BLL.Enums;
using Orders.BLL.Services.Contrancts;
using Orders.DAL.Models;
using Orders.DAL.Pagination.Parameters;
using Orders.DAL.Repositories;
using Orders.DAL.Repositories.Contracts;
using Orders.DAL.Specification;
using Orders.DAL.UOF;

namespace Orders.BLL.Services;

public class OrderService : IOrderService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    

    public OrderService(
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ShortOrderResponse>> GetAllOrdersAsync(OrderParameters orderParameters)
    {
        //var spec = new BaseSpecification<Order>();
        //var test_spec = new OrderByLineTotalOrderSpecification();
        //test_spec.Includes.Add(x => x.LineTotal);
        //var test = await _unitOfWork.OrderRepository.FindWithSpecification(spec);
        //test.Select(x => x.LineTotal);
        
        
        ISpecification<Order> spec = new DefaultOrderSpecification();
        if (orderParameters.OrderBy == "date")
        {
            spec = new OrderByDateOrderSpecification(o => o.Status == OrderStatus.Delivered);
        }
        else if (orderParameters.OrderBy == "lineTotal")
        {
            spec = new OrderByLineTotalOrderSpecification();
        }
        
        
        var result = await _unitOfWork.OrderRepository.FindWithSpecification(spec);
        var orders = _mapper.Map<IEnumerable<ShortOrderResponse>>(result);
        _unitOfWork.CompleteAsync();
        return orders;
    }

    public async Task<OrderResponse> GetOrderByIdAsync(int orderId)
    {
        var result = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
        var order = _mapper.Map<OrderResponse>(result);
        _unitOfWork.CompleteAsync();
        return order;
    }

    public async Task<OrderResponse> AddOrderAsync(OrderRequest orderRequest)
    {
        var order = _mapper.Map<Order>(orderRequest);
        order = await _unitOfWork.OrderRepository.AddAsync(order);
        _unitOfWork.CompleteAsync();
        
        var result = _mapper.Map<OrderResponse>(order);
        return result;
    }

    public async Task<OrderResponse> UpdateOrderAsync(OrderRequest orderRequest)
    {
        var order = _mapper.Map<Order>(orderRequest);
        order = await _unitOfWork.OrderRepository.UpdateAsync(order);
        _unitOfWork.CompleteAsync();
        
        var result = _mapper.Map<OrderResponse>(order);
        return result;
    }

    public async Task DeleteOrderAsync(int id)
    {
        await _unitOfWork.OrderRepository.DeleteByIdAsync(id);
        _unitOfWork.CompleteAsync();
    }
    public async Task ChangeStatus(int id, OrderStatus newStatus)
    {
        // Получаем заказ из базы данных
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);

        if (order == null)
        {
            throw new ArgumentException($"Order with id {id} not found.");
        }

        order.Status = newStatus;

        await _unitOfWork.OrderRepository.UpdateAsync(order);
    }
}