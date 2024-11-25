using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public int OrderId { get; set; }

        public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetOrderByIdHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _context.Order
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

                if (order == null)
                {
                    throw new Exception($"Order with Id {request.OrderId} not found.");
                }

                return _mapper.Map<OrderDto>(order);
            }
        }
    }
}
