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
    public class AddPromotionToProductCommand : IRequest
    {
        public int ProductId { get; set; }
        public PromotionDto PromotionDto { get; set; }

        public AddPromotionToProductCommand(int productId, PromotionDto promotionDto)
        {
            ProductId = productId;
            PromotionDto = promotionDto;
        }

        public class AddPromotionToProductHandler : IRequestHandler<AddPromotionToProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public AddPromotionToProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddPromotionToProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Product.Include(p => p.Promotions).FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);
                if (product == null) throw new Exception("Product not found.");

                var promotion = new Domain.Entitites.Promotion
                {
                    ProductId = request.PromotionDto.ProductId,
                    DiscountPercentage = request.PromotionDto.DiscountPercentage,
                    StartDate = request.PromotionDto.StartDate,
                    EndDate = request.PromotionDto.EndDate
                };

                product.Promotions.Add(promotion);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }

}
