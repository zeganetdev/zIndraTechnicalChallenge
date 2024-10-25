using System.Linq.Expressions;

namespace zIndraTechnicalChallenge.Domain.MainContext.SeedWork
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> FindAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> ToListAsync(CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<IList<T>> WhereToListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<T> SingleOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<T> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T item);
        void UpdateRange(IEnumerable<T> entities);
        IEnumerable<T> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<T, KProperty>> orderByExpression, bool ascending);
        Task<IEnumerable<T>> GetPagedAsync<KProperty>(int pageIndex, int pageCount, Expression<Func<T, bool>> whereExpression, Expression<Func<T, KProperty>> orderByExpression, bool ascending, CancellationToken cancellationToken = default);
    }
}
