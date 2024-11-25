using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Interfaces;

namespace Catalog.DataAccessLayer.Repositories
{
    public class ProducttagRepository : GenericRepository<Producttag>, IProducttagRepository
    {
        public ProducttagRepository(CatalogContext databaseContext) : base(databaseContext)
        {
            
        }

    }
}
