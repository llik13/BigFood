using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.ProductTag.Commands
{
    public class DeleteProductTagCommand : IRequest
    {
        public int TagId { get; set; }

        public class DeleteProductTagHandler : IRequestHandler<DeleteProductTagCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteProductTagHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteProductTagCommand request, CancellationToken cancellationToken)
            {
                var tag = await _context.ProductTags.FindAsync(new object[] { request.TagId }, cancellationToken);

                if (tag == null)
                {
                    throw new Exception($"Tag with Id {request.TagId} not found.");
                }

                _context.ProductTags.Remove(tag);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
