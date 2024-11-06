using Admin.Web.Protos;
using Aplication.Category;
using Aplication.Product;
using Domain.Entitites;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrpcControllerProduct : ControllerBase
    {
        private readonly Products.ProductsClient _grpcClient;

        public GrpcControllerProduct(Products.ProductsClient productsClient)
        {
            _grpcClient = productsClient;
        }

        [HttpGet]
        public async Task<ActionResult<ProductList>> GetProducts()
        {
            var emptyRequest = new Empty();
            var products = await _grpcClient.GetProductsAsync(emptyRequest);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductDto request)
        {
            var product = new ProductDTOs
            {
                ProductId = request.ProductId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId ?? throw new Exception("null value in category id"), 
                ImageUrl = request.ImageUrl,
                Availability = request.Availability ?? throw new Exception("null value in Availability")
            };
            await _grpcClient.AddProductAsync(product);
            return Ok(product);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(ProductDto request)
        {
            var product = new ProductDTOs
            {
                ProductId = request.ProductId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId ?? throw new Exception("null value in category id"),
                ImageUrl = request.ImageUrl,
                Availability = request.Availability ?? throw new Exception("null value in Availability")
            };
            await _grpcClient.UpdateProductAsync(product);
            return Ok(product);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(ProductDto request)
        {
            var product = new ProductDTOs
            {
                ProductId = request.ProductId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId ?? throw new Exception("null value in category id"),
                ImageUrl = request.ImageUrl,
                Availability = request.Availability ?? throw new Exception("null value in Availability")
            };

            await _grpcClient.DeleteProductAsync(product);
            return Ok();
        }
    }
}
