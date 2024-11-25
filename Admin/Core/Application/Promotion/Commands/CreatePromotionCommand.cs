using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Promotion.Commands
{
    public class CreatePromotionCommand : IRequest
    {
        public int ProductId { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class CreatePromotionHandler : IRequestHandler<CreatePromotionCommand>
        {
            private readonly IApplicationDbContext _context;

            public CreatePromotionHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
            {
                var promotion = new Domain.Entitites.Promotion
                {
                    ProductId = request.ProductId,
                    DiscountPercentage = request.DiscountPercentage,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                };

                await _context.Promotion.AddAsync(promotion, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
