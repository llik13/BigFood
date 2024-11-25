using Aplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Product.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public double? Rating { get; set; }
        public string? ImageUrl { get; set; }
        public bool? Availability { get; set; }

        public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateProductHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Product.FindAsync(new object[] { request.ProductId }, cancellationToken);

                if (product == null)
                {
                    throw new Exception($"Product with Id {request.ProductId} not found.");
                }

                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.CategoryId = request.CategoryId;
                product.Rating = request.Rating;
                product.ImageUrl = request.ImageUrl;
                product.Availability = request.Availability;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
