using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Product.Commands
{
    public class CreateProductCommand : IRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public double? Rating { get; set; }
        public string? ImageUrl { get; set; }
        public bool? Availability { get; set; }

        public class CreateProductHandler : IRequestHandler<CreateProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public CreateProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var product = new Domain.Entitites.Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    CategoryId = request.CategoryId,
                    Rating = request.Rating,
                    ImageUrl = request.ImageUrl,
                    Availability = request.Availability
                };

                await _context.Product.AddAsync(product, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
