using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Product.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public int ProductId { get; set; }

        public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Product.FindAsync(new object[] { request.ProductId }, cancellationToken);

                if (product == null)
                {
                    throw new Exception($"Product with Id {request.ProductId} not found.");
                }

                _context.Product.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
