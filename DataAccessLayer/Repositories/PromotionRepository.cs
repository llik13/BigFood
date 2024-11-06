using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Interfaces;

namespace Catalog.DataAccessLayer.Repositories
{
    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
    {
        public PromotionRepository(CatalogContext databaseContext) : base(databaseContext)
        {
        }
    }
}
