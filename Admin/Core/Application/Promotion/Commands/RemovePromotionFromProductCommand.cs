using Aplication.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Promotion.Commands
{
    public class RemovePromotionFromProductCommand : IRequest
    {
        public int ProductId { get; set; }
        public int PromotionId { get; set; }

        public RemovePromotionFromProductCommand(int productId, int promotionId)
        {
            ProductId = productId;
            PromotionId = promotionId;
        }

        public class RemovePromotionFromProductHandler : IRequestHandler<RemovePromotionFromProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public RemovePromotionFromProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemovePromotionFromProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Product.Include(p => p.Promotions).FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);
                if (product == null) throw new Exception("Product not found.");

                var promotion = product.Promotions.FirstOrDefault(p => p.PromotionId == request.PromotionId);
                if (promotion == null) throw new Exception("Promotion not found for this product.");

                product.Promotions.Remove(promotion);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }

}
