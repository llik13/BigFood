using Aplication.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.ProductTag.Commands
{
    public class RemoveTagFromProductCommand : IRequest
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }

        public RemoveTagFromProductCommand(int productId, int tagId)
        {
            ProductId = productId;
            TagId = tagId;
        }

        public class RemoveTagFromProductHandler : IRequestHandler<RemoveTagFromProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public RemoveTagFromProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveTagFromProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Product.Include(p => p.ProductTags)
                    .FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);
                if (product == null) throw new Exception("Product not found.");

                var tag = product.ProductTags.FirstOrDefault(t => t.TagId == request.TagId);
                if (tag == null) throw new Exception("Tag not found on this product.");

                product.ProductTags.Remove(tag);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }

}
