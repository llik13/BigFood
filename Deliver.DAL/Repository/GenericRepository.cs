using Deliver.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.DAL.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DeliverContext databaseContext;
        protected readonly DbSet<T> table;

        protected GenericRepository(DeliverContext databaseContext)
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
            databaseContext.SaveChanges();
        }

        public virtual async Task AddAsync(T t)
        {
            await table.AddAsync(t);
            databaseContext.SaveChanges();
        }

        public virtual async Task ReplaceAsync(T t)
        {
            await Task.Run(() => table.Update(t));
            databaseContext.SaveChanges();
        }

    }
}
