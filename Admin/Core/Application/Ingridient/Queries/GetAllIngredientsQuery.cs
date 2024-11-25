using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Ingridient.Queries
{
    public class GetAllIngredientsQuery : IRequest<List<IngredientDto>>
    {
        public class GetAllIngredientsHandler : IRequestHandler<GetAllIngredientsQuery, List<IngredientDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAllIngredientsHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<IngredientDto>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
            {
                var ingredients = await _context.Ingridient.ToListAsync(cancellationToken);
                return _mapper.Map<List<IngredientDto>>(ingredients);
            }
        }
    }
}
