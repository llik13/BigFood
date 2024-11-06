namespace Review.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICommentRepository _commentRepository { get; }
        IReviewRepository _reviewRepository { get; }
        ILikeRepository _likeRepository { get; }
        IUserRepository _userRepository { get; }
        void Commit();
        void Dispose();
    }
}
