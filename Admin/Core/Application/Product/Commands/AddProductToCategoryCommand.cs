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
    public class AddProductToCategoryCommand : IRequest
    {
        public int CategoryId { get; set; }
        public Domain.Entitites.Product Product { get; set; }

        public AddProductToCategoryCommand(int categoryId, Domain.Entitites.Product product)
        {
            CategoryId = categoryId;
            Product = product;
        }

        public class AddProductToCategoryHandler : IRequestHandler<AddProductToCategoryCommand>
        {
            private readonly IApplicationDbContext _context;

            public AddProductToCategoryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddProductToCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _context.Category.Include(c => c.Products).FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);
                if (category == null) throw new Exception("Category not found.");

                // Устанавливаем идентификатор категории для нового продукта
                request.Product.CategoryId = category.CategoryId;

                category.Products.Add(request.Product);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }

}
