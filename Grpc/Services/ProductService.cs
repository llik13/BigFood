using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Grpc.Services
{
    public class ProductService : Products.ProductsBase
    {
        private readonly CatalogContext _dbContext;

        public ProductService(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<ProductList> GetProducts(Empty request, ServerCallContext context)
        {
            var products = await _dbContext.Products.ToListAsync();
            var productList = new ProductList();

            productList.Products.AddRange(products.Select(c => new ProductDTOs
            {
                ProductId = c.ProductId,
                Name = c.Name,
                Description = c.Description,
                Price = (DecimalValue) c.Price,
                CategoryId = c.CategoryId ?? 0,
                ImageUrl = c.ImageUrl,
                Availability = c.Availability ?? false
            }));
            
            return productList;
        }
        
        public override async Task<Empty> UpdateProduct(ProductDTOs request, ServerCallContext context)
        {
            var product = new Product
            {
                ProductId = request.ProductId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
                ImageUrl = request.ImageUrl,
                Availability = request.Availability 
            };

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return new Empty();
        }

        public override async Task<Empty> DeleteProduct(ProductDTOs request, ServerCallContext context)
        {
            var product = new Product
            {
                ProductId = request.ProductId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
                ImageUrl = request.ImageUrl,
                Availability = request.Availability
            };

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return new Empty();
        }

        public override async Task<Empty> AddProduct(ProductDTOs request, ServerCallContext context)
        {
            var product = new Product
            {
                ProductId = request.ProductId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
                ImageUrl = request.ImageUrl,
                Availability = request.Availability
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return new Empty();
        }
    }
}
