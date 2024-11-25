using CourierService.Application.Common.Interfaces;

namespace CourierService.Application.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrderDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OrderDetailsDTO> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Where(o => o.Id == request.Id)
            .Include(o => o.Product)
            .Include(o => o.User)
            .FirstOrDefaultAsync(cancellationToken);
        var orderDetails = _mapper.Map<OrderDetailsDTO>(order);
        return orderDetails;
    }
}
