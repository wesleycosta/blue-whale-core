using PixelHotel.Core.Domain;
using System.Linq.Expressions;

namespace PixelHotel.Core.Abstractions;

public interface IRepositoryBase<TEntity> where TEntity : Entity
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    Task Remove(Guid id);
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TResult>> GetByExpression<TResult>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> projection);
}
