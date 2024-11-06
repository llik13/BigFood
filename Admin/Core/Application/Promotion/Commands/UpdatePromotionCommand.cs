using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Promotion.Commands
{
    public class UpdatePromotionCommand : IRequest
    {
        public int PromotionId { get; set; }
        public int ProductId { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class UpdatePromotionHandler : IRequestHandler<UpdatePromotionCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdatePromotionHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
            {
                var promotion = await _context.Promotion.FindAsync(new object[] { request.PromotionId }, cancellationToken);

                if (promotion == null)
                {
                    throw new Exception($"Promotion with Id {request.PromotionId} not found.");
                }

                promotion.ProductId = request.ProductId;
                promotion.DiscountPercentage = request.DiscountPercentage;
                promotion.StartDate = request.StartDate;
                promotion.EndDate = request.EndDate;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
