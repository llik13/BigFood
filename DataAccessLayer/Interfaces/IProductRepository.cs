using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Pagination;
using Catalog.DataAccessLayer.Pagination.Parametrs;

namespace Catalog.DataAccessLayer.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PagedList<Product>> GetProductsAsync(ProductParametrs productParametrs);
        Task<Product> GetProdByIdWithPromAsync(int id);
        Task<Product> GetProdByIdWithTagAsync(int id);
    }
}
