using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Category.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int CategoryId { get; set; }
        public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetCategoryByIdHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var category = await _context.Category
                    .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);

                if (category == null)
                {
                    throw new Exception($"Category with Id {request.CategoryId} not found.");
                }

                return _mapper.Map<CategoryDto>(category);
            }
        }
    }
}
