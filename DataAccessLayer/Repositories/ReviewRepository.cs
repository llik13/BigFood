using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Repositories;
using Catalog.DataAccessLayer.Interfaces;

namespace Catalog.DataAccessLayer.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(CatalogContext databaseContext) : base(databaseContext)
        {
        }
    }
}
