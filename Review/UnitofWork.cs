using Review.Interfaces;
using System.Data;

namespace Review
{
    public class UnitofWork : IUnitOfWork, IDisposable
    {
        public ICommentRepository _commentRepository { get; }
        public IReviewRepository _reviewRepository { get; }
        public ILikeRepository _likeRepository { get; }
        public IUserRepository _userRepository { get; }

        public IDbTransaction _dbTransaction;

        public UnitofWork(ICommentRepository commentRepository, IReviewRepository reviewRepository, ILikeRepository likeRepository, IUserRepository userRepository, IDbTransaction dbTransaction)
        {
            _commentRepository = commentRepository;
            _reviewRepository = reviewRepository;
            _likeRepository = likeRepository;
            _userRepository = userRepository;
            _dbTransaction = dbTransaction;
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}
