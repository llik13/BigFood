using Aggregator.Models;
using Aggregator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IReviewService _reviewService;

        public CatalogController(IProductService productService, ICategoryService categoryService, IReviewService reviewService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService)); ;
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
        }

        [HttpGet]
        public async Task<ActionResult<CatalogModel>> GetCatalog() 
        {
            var products = await _productService.GetFullProductsInformationAsync();
            var categories = await _categoryService.GetCategoriesAsync();
            CatalogModel catalog = new CatalogModel
            {
                Product = products,
                Category = categories
            };
            return Ok(catalog);
        }

        [HttpGet("FullProduct")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var products = await _productService.GetFullProductsInformationAsync();
            var reviews = await _reviewService.GetReviewsAsync();

            var listFullProducts = new List<ProductModel>();

            var reviewDictionary = reviews.ToDictionary(r => r.Id);

            foreach (var product in products)
            {
                listFullProducts.Add(new ProductModel
                {
                    Product = product,
                    Review = reviewDictionary.ContainsKey(product.ProductId) ? reviewDictionary[product.ProductId] : null
                });
            }
            return listFullProducts;
        }


    }
}
