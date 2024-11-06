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
    public class AddTagToProductCommand : IRequest
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }

        public AddTagToProductCommand(int productId, int tagId)
        {
            ProductId = productId;
            TagId = tagId;
        }

        public class AddTagToProductHandler : IRequestHandler<AddTagToProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public AddTagToProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddTagToProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Product.Include(p => p.ProductTags)
                    .FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);
                if (product == null) throw new Exception("Product not found.");

                var tag = await _context.ProductTags.FindAsync(request.TagId);
                if (tag == null) throw new Exception("Tag not found.");

                product.ProductTags.Add(tag);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }

}
