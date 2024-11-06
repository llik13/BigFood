using Aplication.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Ingridient.Commands
{
    public class RemoveIngredientFromProductCommand : IRequest
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }

        public RemoveIngredientFromProductCommand(int productId, int ingredientId)
        {
            ProductId = productId;
            IngredientId = ingredientId;
        }

        public class RemoveIngredientFromProductHandler : IRequestHandler<RemoveIngredientFromProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public RemoveIngredientFromProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RemoveIngredientFromProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Product.Include(p => p.ProductIngredients).FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);
                if (product == null) throw new Exception("Product not found.");

                var ingredient = product.ProductIngredients.FirstOrDefault(i => i.IngredientID == request.IngredientId);
                if (ingredient == null) throw new Exception("Ingredient not found in this product.");

                product.ProductIngredients.Remove(ingredient);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }

}
