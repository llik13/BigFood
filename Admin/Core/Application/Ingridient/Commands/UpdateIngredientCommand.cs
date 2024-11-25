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
    public class UpdateIngredientCommand : IRequest
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }

        public class UpdateIngredientHandler : IRequestHandler<UpdateIngredientCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateIngredientHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
            {
                var ingredient = await _context.Ingridient.FindAsync(new object[] { request.IngredientID }, cancellationToken);

                if (ingredient == null)
                {
                    throw new Exception($"Ingridient with Id {request.IngredientID} not found.");
                }

                ingredient.Name = request.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
