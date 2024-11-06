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
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int ProductId { get; set; }

        public GetProductByIdQuery(int productId)
        {
            ProductId = productId;
        }

        public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetProductByIdHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await _context.Product
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);

                if (product == null)
                {
                    throw new Exception($"Product with Id {request.ProductId} not found.");
                }

                return _mapper.Map<ProductDto>(product);
            }
        }
    }

}
