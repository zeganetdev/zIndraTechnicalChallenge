using Microsoft.EntityFrameworkCore.Storage;
using zIndraTechnicalChallenge.Domain.MainContext.Aggregates.ProductAgg;

namespace zIndraTechnicalChallenge.Domain.MainContext.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository ProductRepository { get; }
        IRepository<T> Repository<T>() where T : Entity;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        IDbContextTransaction BeginTransaction();
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
        void RollbackTransaction();
    }
}
