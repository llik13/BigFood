using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Repositories;
using Catalog.DataAccessLayer.Interfaces;

namespace Catalog.DataAccessLayer.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogContext context) : base(context)
        {
        }
    }
}
