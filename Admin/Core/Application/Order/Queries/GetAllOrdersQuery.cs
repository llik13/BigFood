using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Order.Queries
{
    public class GetAllOrdersQuery : IRequest<List<OrderDto>>
    {
        public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAllOrdersHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
            {
                var orders = await _context.Order
                    .Include(o => o.User)
                    .ToListAsync(cancellationToken);

                return _mapper.Map<List<OrderDto>>(orders);
            }
        }
    }
}
