using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.ProductTag.Queries
{
    public class GetAllProductTagsQuery : IRequest<List<ProductTagDto>>
    {
        public class GetAllProductTagsHandler : IRequestHandler<GetAllProductTagsQuery, List<ProductTagDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAllProductTagsHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ProductTagDto>> Handle(GetAllProductTagsQuery request, CancellationToken cancellationToken)
            {
                var tags = await _context.ProductTags.ToListAsync(cancellationToken);
                return _mapper.Map<List<ProductTagDto>>(tags);
            }
        }
    }

}
