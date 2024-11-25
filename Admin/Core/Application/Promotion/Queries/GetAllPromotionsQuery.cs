using Aplication.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Promotion.Queries
{
    public class GetAllPromotionsQuery : IRequest<List<PromotionDto>>
    {
        public class GetAllPromotionsHandler : IRequestHandler<GetAllPromotionsQuery, List<PromotionDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetAllPromotionsHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<PromotionDto>> Handle(GetAllPromotionsQuery request, CancellationToken cancellationToken)
            {
                var promotions = await _context.Promotion.ToListAsync(cancellationToken);
                return _mapper.Map<List<PromotionDto>>(promotions);
            }
        }
    }

}
