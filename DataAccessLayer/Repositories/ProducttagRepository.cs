using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Catalog.DataAccessLayer.Repositories
{
    public class ProducttagRepository : GenericRepository<Producttag>, IProducttagRepository
    {
        public ProducttagRepository(CatalogContext databaseContext) : base(databaseContext)
        {
            
        }

    }
}
