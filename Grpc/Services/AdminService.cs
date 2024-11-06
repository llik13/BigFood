using Grpc.Core;

using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;
using Grpc.Protos;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Grpc.Services
{
    public class AdminService : Categories.CategoriesBase
    {
        private readonly CatalogContext _dbContext;

        public AdminService(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<CategoryList> GetCategory(Empty request, ServerCallContext context)
        {
            var categories = await _dbContext.Categories.ToListAsync();

            var categoryList = new CategoryList();
            categoryList.Categories.AddRange(categories.Select(c => new CategoryDTOs // Используйте правильный тип
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl
            }));

            return categoryList;
        }

        public override async Task<Empty> UpdateCategory(CategoryDTOs request, ServerCallContext context)
        {
            var category = new Category
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl
            };

            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();

            return new Empty();
        }

        public override async Task<Empty> DeleteCategory(CategoryDTOs request, ServerCallContext context)
        {
            var category = new Category
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl
            };

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return new Empty();
        }

        public override async Task<Empty> AddCategory(CategoryDTOs request, ServerCallContext context)
        {
            var category = new Category
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return new Empty();
        }
    }
    
}
