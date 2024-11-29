using Deliver.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DeliverContext _deliverContext;
        private IDbContextTransaction transaction;

        public IDeliverRepository _deliverRepository { get; }

        public UnitOfWork(DeliverContext deliverContext, IDeliverRepository deliverRepository)
        {
            _deliverContext = deliverContext;
            _deliverRepository = deliverRepository;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            transaction = await _deliverContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            await transaction.RollbackAsync(cancellationToken);
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _deliverContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            transaction?.Dispose();

            _deliverContext?.Dispose();
        }
    }
}
