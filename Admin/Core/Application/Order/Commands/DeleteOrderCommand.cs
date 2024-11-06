using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Order.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public int OrderId { get; set; }
    }

    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOrderHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Order.FindAsync(new object[] { request.OrderId }, cancellationToken);

            if (order == null)
            {
                throw new Exception($"Order with Id {request.OrderId} not found.");
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
