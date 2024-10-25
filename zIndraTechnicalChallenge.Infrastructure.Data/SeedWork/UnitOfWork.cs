using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;
using zIndraTechnicalChallenge.Domain.MainContext.Aggregates.ProductAgg;
using zIndraTechnicalChallenge.Domain.MainContext.SeedWork;
using zIndraTechnicalChallenge.Infrastructure.Data.Context;
using zIndraTechnicalChallenge.Infrastructure.Data.Repositories;

namespace zIndraTechnicalChallenge.Infrastructure.Data.SeedWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IProductRepository _productRepository;

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.RollbackTransactionAsync(cancellationToken);
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
        private Hashtable _repositories;

        public IRepository<T> Repository<T>() where T : Entity
        {
            if (_repositories == null) _repositories = new Hashtable();
            var key = typeof(T).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repositoryType = typeof(IRepository<>);
                var repositoryIntance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(key, repositoryIntance);
            }
            return (IRepository<T>)_repositories[key];
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
