namespace Forum.DAL.Repositories.Contracts;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task DeleteAsync(int id);
    Task<T> GetAsync(int id);
    Task<int> AddRangeAsync(IEnumerable<T> list);
    Task ReplaceAsync(T t, int? id = null);
    Task<int> AddAsync(T t);
}
