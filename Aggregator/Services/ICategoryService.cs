using Aggregator.Models;

namespace Aggregator.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
    }

}
