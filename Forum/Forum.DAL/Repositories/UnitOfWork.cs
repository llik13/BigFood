using System.Data;
using Forum.DAL.Repositories.Contracts;

namespace Forum.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    
        public ICategoryRepository _categoryRepository { get; }
        public ICommentRepository _commentRepository { get; }
        public IPostRepository _postRepository { get; }
        public IUserRepository _userRepository { get; }

        readonly IDbTransaction _dbTransaction;

        public UnitOfWork(IPostRepository postRepository, 
            ICategoryRepository categoryRepository, 
            ICommentRepository commentRepository,
            IUserRepository userRepository,
            IDbTransaction dbTransaction)
        {
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _dbTransaction = dbTransaction;
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
                // By adding this we can have muliple transactions as part of a single request
                //_dbTransaction.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
}