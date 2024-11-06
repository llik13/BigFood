using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Product.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
        public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAllProductsHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var products = await _context.Product
                    .Include(p => p.Category)
                    .ToListAsync(cancellationToken);

                return _mapper.Map<List<ProductDto>>(products);
            }
        }
    }

}
