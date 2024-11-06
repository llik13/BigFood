using Aggregator.Models;
using Aggregator.Extensions;

namespace Aggregator.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductDTO>> GetFullProductsInformationAsync()
        {
            var response = await _client.GetAsync($"/api/Product/full");
            response.EnsureSuccessStatusCode();
            return await response.ReadContentAs<List<ProductDTO>>();
        }
    }
}
