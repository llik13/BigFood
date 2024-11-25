using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.ProductTag.Queries
{
    public class GetProductTagByIdQuery : IRequest<ProductTagDto>
    {
        public int TagId { get; set; }

        public GetProductTagByIdQuery(int tagId)
        {
            TagId = tagId;
        }

        public class GetProductTagByIdHandler : IRequestHandler<GetProductTagByIdQuery, ProductTagDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetProductTagByIdHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ProductTagDto> Handle(GetProductTagByIdQuery request, CancellationToken cancellationToken)
            {
                var tag = await _context.ProductTags
                    .Where(t => t.TagId == request.TagId)
                    .FirstOrDefaultAsync(cancellationToken); 

                if (tag == null)
                {
                    throw new Exception($"Tag with Id {request.TagId} not found.");
                }

                return _mapper.Map<ProductTagDto>(tag);
            }
        }
    }

}
