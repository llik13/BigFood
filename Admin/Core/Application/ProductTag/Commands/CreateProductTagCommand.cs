using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.ProductTag.Commands
{
    public class CreateProductTagCommand : IRequest
    {
        public string TagName { get; set; } = null!;

        public class CreateProductTagHandler : IRequestHandler<CreateProductTagCommand>
        {
            private readonly IApplicationDbContext _context;

            public CreateProductTagHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateProductTagCommand request, CancellationToken cancellationToken)
            {
                var tag = new Domain.Entitites.ProductTag
                {
                    TagName = request.TagName
                };

                await _context.ProductTags.AddAsync(tag, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
