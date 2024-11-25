using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Interfaces;
using Catalog.DataAccessLayer.Pagination;
using Catalog.DataAccessLayer.Pagination.Parametrs;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;

namespace Catalog.DataAccessLayer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private ISortHelper<Product> _sortHelper;
        public ProductRepository(CatalogContext databaseContext, ISortHelper<Product> sortHelper) : base(databaseContext)
        {
            _sortHelper = sortHelper;
        }

        public async Task<PagedList<Product>> GetProductsAsync(ProductParametrs productParametrs)
        {
            var productsQuery = databaseContext.Products.Include(p => p.Promotions)
            .Include(p => p.Producttags)
            .AsQueryable();

            
            if (productParametrs.MinCost > 0 || productParametrs.MaxCost > 0)
            {
                productsQuery = productsQuery.Where(p =>
                    (productParametrs.MinCost == 0 || p.Price >= productParametrs.MinCost) &&
                    (productParametrs.MaxCost == 0 || p.Price <= productParametrs.MaxCost));
            }

         
            if (!string.IsNullOrEmpty(productParametrs.Name))
            {
                SearchByName(ref productsQuery, productParametrs.Name);
            }

            if (!string.IsNullOrEmpty(productParametrs.OrderBy))
            {
                productsQuery = _sortHelper.ApplySort(ref productsQuery, productParametrs.OrderBy);
            }

           
            var products = await productsQuery.ToListAsync();

            var pagProd = PagedList<Product>.ToPagedList(products.AsQueryable(),
                productParametrs.PageNumber,
                productParametrs.PageSize);

            return pagProd;
        }

        private void SearchByName(ref IQueryable<Product> products, string productName)
        {
            products = products.Where(o => o.Name.ToLower().Contains(productName.Trim().ToLower()));
        }

        public async Task<Product> GetProdByIdWithPromAsync(int id)
        {
            return databaseContext.Products.Include(p => p.Promotions).FirstOrDefault(p => p.ProductId == id);
        }

        public async Task<Product> GetProdByIdWithTagAsync(int id)
        {
            return databaseContext.Products.Include(p => p.Producttags).FirstOrDefault(p => p.ProductId == id);
        }

    }
}
