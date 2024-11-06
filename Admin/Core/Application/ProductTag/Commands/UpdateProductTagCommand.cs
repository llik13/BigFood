using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.ProductTag.Commands
{
    public class UpdateProductTagCommand : IRequest
    {
        public int TagId { get; set; }
        public string TagName { get; set; } = null!;

        public class UpdateProductTagHandler : IRequestHandler<UpdateProductTagCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateProductTagHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateProductTagCommand request, CancellationToken cancellationToken)
            {
                var tag = await _context.ProductTags.FindAsync(new object[] { request.TagId }, cancellationToken);

                if (tag == null)
                {
                    throw new Exception($"Tag with Id {request.TagId} not found.");
                }

                tag.TagName = request.TagName;
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
