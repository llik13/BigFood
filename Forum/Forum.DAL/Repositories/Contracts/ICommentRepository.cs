using Forum.DAL.Entities;

namespace Forum.DAL.Repositories.Contracts;

public interface ICommentRepository : IGenericRepository<Comment>
{
    Task<IEnumerable<Comment>> GetByPostId(int post_id);
    Task<IEnumerable<Comment>> GetByUserId(int user_id);
}