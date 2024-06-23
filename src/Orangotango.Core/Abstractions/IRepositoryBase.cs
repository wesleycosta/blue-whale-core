using Orangotango.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Orangotango.Core.Abstractions;

public interface IRepositoryBase<TEntity> where TEntity : EntityBase
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void SoftDelete(TEntity entity);
    void HardDelete(TEntity entity);
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TResult>> GetByExpression<TResult>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> projection);
    Task<TResult> GetFirstByExpression<TResult>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> projection);
    Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
}
