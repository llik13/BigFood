namespace Forum.DAL.Repositories.Contracts;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository _categoryRepository { get; }
    ICommentRepository _commentRepository { get; }
    IPostRepository _postRepository { get; }
    IUserRepository _userRepository { get; }

    void Commit();
    
    void Dispose();
}
