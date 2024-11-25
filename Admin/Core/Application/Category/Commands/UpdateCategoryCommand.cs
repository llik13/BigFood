using Aplication.Interfaces;
using MediatR;

namespace Aplication.Category.Commands
{
    public class UpdateCategoryCommand : IRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateCategoryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _context.Category.FindAsync(new object[] { request.CategoryId }, cancellationToken);

                if (category == null)
                {
                    throw new Exception($"Category with Id {request.CategoryId} not found.");
                }

                category.Name = request.Name;
                category.Description = request.Description;
                category.ImageUrl = request.ImageUrl;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
