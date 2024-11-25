using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Promotion.Commands
{
    public class DeletePromotionCommand : IRequest
    {
        public int PromotionId { get; set; }

        public class DeletePromotionHandler : IRequestHandler<DeletePromotionCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeletePromotionHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
            {
                var promotion = await _context.Promotion.FindAsync(new object[] { request.PromotionId }, cancellationToken);

                if (promotion == null)
                {
                    throw new Exception($"Promotion with Id {request.PromotionId} not found.");
                }

                _context.Promotion.Remove(promotion);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
