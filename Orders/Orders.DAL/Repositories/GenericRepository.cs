using Microsoft.EntityFrameworkCore;
using Orders.DAL.Models;
using Orders.DAL.Repositories.Contracts;
using Orders.DAL.Specification;

namespace Orders.DAL.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly OrdersContext context;
    protected readonly DbSet<T> table;

    public GenericRepository(OrdersContext context)
    {
        this.context = context;
        table = context.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await table.ToListAsync(); 
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await table.FindAsync(id);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException();
        }
        var addedEntity = await table.AddAsync(entity);
        context.SaveChanges();
        return addedEntity.Entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException();
        }
        var updatedEntity = context.Update(entity);
        context.SaveChanges();
        return await Task.Run(() => updatedEntity.Entity);

    }

    public virtual async Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        await Task.Run(() => table.Remove(entity));
    }

    public virtual async Task DeleteAsync(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException();
        }
        await Task.Run(() => table.Remove(entity));
        context.SaveChanges();
    }

    public async Task<IEnumerable<T>> FindWithSpecification(ISpecification<T> specification)
    {
        return await Task.Run(() => SpecificationEvaluator<T>.GetQuery(table.AsQueryable(), specification));
    }
}