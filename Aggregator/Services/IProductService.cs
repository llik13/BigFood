using Aggregator.Models;

namespace Aggregator.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetFullProductsInformationAsync( );
    }

}
