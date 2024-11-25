using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Category.Commands
{
    public class DeleteCategoryCommand : IRequest
    {
        public int CategoryId { get; set; }

        public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteCategoryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _context.Category.FindAsync(new object[] { request.CategoryId }, cancellationToken);

                if (category == null)
                {
                    throw new Exception($"Category with Id {request.CategoryId} not found.");
                }

                _context.Category.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
