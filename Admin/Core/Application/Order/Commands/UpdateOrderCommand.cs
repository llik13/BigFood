using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Order.Commands
{
    public class UpdateOrderCommand : IRequest
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateOrderHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _context.Order.FindAsync(new object[] { request.OrderId }, cancellationToken);

                if (order == null)
                {
                    throw new Exception($"Order with Id {request.OrderId} not found.");
                }

                order.TotalPrice = request.TotalPrice;
                order.Status = request.Status;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
