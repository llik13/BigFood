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
    public class GetPromotionByIdQuery : IRequest<PromotionDto>
    {
        public int PromotionId { get; set; }

        public GetPromotionByIdQuery(int promotionId)
        {
            PromotionId = promotionId;
        }

        public class GetPromotionByIdHandler : IRequestHandler<GetPromotionByIdQuery, PromotionDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetPromotionByIdHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PromotionDto> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
            {
                var promotion = await _context.Promotion
                    .FirstOrDefaultAsync(p => p.PromotionId == request.PromotionId, cancellationToken);

                if (promotion == null)
                {
                    throw new Exception($"Promotion with Id {request.PromotionId} not found.");
                }

                return _mapper.Map<PromotionDto>(promotion);
            }
        }
    }

}
