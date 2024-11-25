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
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
    {
        public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAllCategoriesHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categories = await _context.Category
                    .ToListAsync(cancellationToken);

                return _mapper.Map<List<CategoryDto>>(categories);
            }
        }
    }
}
