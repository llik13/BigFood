using Forum.DAL.Entities;

namespace Forum.DAL.Repositories.Contracts;

public interface IPostRepository : IGenericRepository<Post>
{
    //Task<ResponsePost> GetAllInfoAsync(int id);
}