namespace Catalog.DataAccessLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task ReplaceAsync(T t);
        Task AddAsync(T t);
    }
}
