using Aplication.Interfaces;
using MediatR;

namespace Aplication.Ingridient.Commands
{
    public class DeleteIngredientCommand : IRequest
    {
        public int IngredientID { get; set; }

        public DeleteIngredientCommand(int IngredientID)
        {
            this.IngredientID = IngredientID;
        }

        public class DeleteIngredientHandler : IRequestHandler<DeleteIngredientCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteIngredientHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
            {
                var ingredient = await _context.Ingridient.FindAsync(new object[] { request.IngredientID }, cancellationToken);

                if (ingredient == null)
                {
                    throw new Exception($"Ingridient with Id {request.IngredientID} not found.");
                }

                _context.Ingridient.Remove(ingredient);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
