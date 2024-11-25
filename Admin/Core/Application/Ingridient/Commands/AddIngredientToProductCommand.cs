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
    public class AddIngredientToProductCommand : IRequest
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }

        public AddIngredientToProductCommand(int productId, int ingredientId)
        {
            ProductId = productId;
            IngredientId = ingredientId;
        }

        public class AddIngredientToProductHandler : IRequestHandler<AddIngredientToProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public AddIngredientToProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddIngredientToProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Product.Include(p => p.ProductIngredients).FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);
                if (product == null) throw new Exception("Product not found.");

                var ingredient = await _context.Ingridient.FindAsync(new object[] { request.IngredientId }, cancellationToken);
                if (ingredient == null) throw new Exception("Ingredient not found.");

                product.ProductIngredients.Add(ingredient);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }

}
