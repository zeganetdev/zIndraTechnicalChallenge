using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using zIndraTechnicalChallenge.Domain.MainContext.SeedWork;

namespace zIndraTechnicalChallenge.Infrastructure.Data.SeedWork
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext Context;

        protected readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            Context = context;
            _entities = Context.Set<TEntity>();
        }

        public async Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _entities.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync(CancellationToken cancellationToken = default)
        {
            return await _entities.ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _entities.CountAsync(cancellationToken);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public async Task<IList<TEntity>> WhereToListAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            return await _entities.Where(spec.SpecExpression).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            return await _entities.SingleOrDefaultAsync(spec.SpecExpression, cancellationToken);
        }

        public async Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            return await _entities.FirstOrDefaultAsync(spec.SpecExpression, cancellationToken);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _entities.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _entities.AddRangeAsync(entities, cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public void Update(TEntity item)
        {
            _entities.Attach(item);
            Context.Entry(item).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.AttachRange(entities);
            foreach (var item in entities)
            {
                Context.Entry(item).State = EntityState.Modified;
            }
        }

        public IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            if (ascending)
            {
                return _entities.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return _entities.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        public async Task<IEnumerable<TEntity>> GetPagedAsync<KProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending, CancellationToken cancellationToken = default)
        {
            if (ascending)
            {
                return await _entities.Where(whereExpression).OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount).ToListAsync(cancellationToken);
            }
            else
            {
                return await _entities.Where(whereExpression).OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount).ToListAsync(cancellationToken);
            }
        }

    }
}
