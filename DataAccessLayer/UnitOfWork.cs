using Catalog.DataAccessLayer.Entities;
using Catalog.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogContext catalogContext;
        private IDbContextTransaction transaction;

        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProducttagRepository ProducttagRepository { get; }
        public IPromotionRepository PromotionRepository { get; }
        public IReviewRepository ReviewRepository { get; }
        private readonly ISortHelper<Product> _sortHelper; // Добавляем поле для SortHelper

        public UnitOfWork(
            CatalogContext catalogContext,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IProducttagRepository producttagRepository,
            IPromotionRepository promotionRepository,
            IReviewRepository reviewRepository,
            ISortHelper<Product> sortHelper) // Передаем SortHelper в конструктор
        {
            this.catalogContext = catalogContext;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            ProducttagRepository = producttagRepository;
            PromotionRepository = promotionRepository;
            ReviewRepository = reviewRepository;
            _sortHelper = sortHelper; // Инициализация SortHelper
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            transaction = await catalogContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync( CancellationToken cancellationToken = default)
        {
            await transaction.RollbackAsync(cancellationToken);
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await catalogContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            transaction?.Dispose();

            catalogContext?.Dispose();
        }
    }
}
