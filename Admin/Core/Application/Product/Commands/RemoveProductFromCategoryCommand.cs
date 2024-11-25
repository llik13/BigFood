using Aplication.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Product.Commands
{
    public class RemoveProductFromCategoryCommand : IRequest
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public RemoveProductFromCategoryCommand(int categoryId, int productId)
        {
            CategoryId = categoryId;
            ProductId = productId;
        }

        public class RemoveProductFromCategoryHandler : IRequestHandler<RemoveProductFromCategoryCommand>
        {
            private readonly IApplicationDbContext _context;

            public RemoveProductFromCategoryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveProductFromCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _context.Category.Include(c => c.Products).FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);
                if (category == null) throw new Exception("Category not found.");

                var product = category.Products.FirstOrDefault(p => p.ProductId == request.ProductId);
                if (product == null) throw new Exception("Product not found in this category.");

                category.Products.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }

}
