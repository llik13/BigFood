using Aplication.Interfaces;
using Domain.Entitites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Ingridient.Commands
{
    public class CreateIngredientCommand : IRequest
    {
        public string Name { get; set; }

        public class CreateIngredientHandler : IRequestHandler<CreateIngredientCommand>
        {
            private readonly IApplicationDbContext _context;

            public CreateIngredientHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
            {
                var ingredient = new Ingredient
                {
                    Name = request.Name
                };

                await _context.Ingridient.AddAsync(ingredient, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
