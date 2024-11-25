using Catalog.BuisnesDataLayer.DTO.Request;
using Catalog.BuisnesDataLayer.DTO.Response;
using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Pagination;
using Catalog.DataAccessLayer.Pagination.Parametrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.BuisnesDataLayer.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductFullResponseDTO>> GetFullProductsInformationAsync(ProductParametrs productParametrs);
        Task<IEnumerable<ProductSummaryResponseDTO>> GetProductSummaryInformationAsync(ProductParametrs productParametrs);
        Task AddProductAsync(ProductRequestDTO product);
        Task RemoveProductAsync(int id );
        Task UpdateProductAsync(ProductRequestDTO product);
    }
}
