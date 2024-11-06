using Aggregator.Models;
using Aggregator.Extensions;

namespace Aggregator.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;

        public CategoryService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var response = await _client.GetAsync("/api/Category");
            response.EnsureSuccessStatusCode();
            return await response.ReadContentAs<List<CategoryDTO>>();
        }
    }

}
