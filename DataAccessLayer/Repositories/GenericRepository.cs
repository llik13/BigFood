using Catalog.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DataAccessLayer.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        protected readonly CatalogContext databaseContext;
        protected readonly DbSet<T> table;

        protected GenericRepository(CatalogContext databaseContext)
        {
            this.databaseContext = databaseContext;
            table = this.databaseContext.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await table.FindAsync(id);
            await Task.Run(() => table.Remove(entity));
        }

        public virtual async Task AddAsync(T t)
        {
            await table.AddAsync(t);
        }

        public virtual async Task ReplaceAsync(T t)
        {
            await Task.Run(() => table.Update(t));
        }

    }
}
