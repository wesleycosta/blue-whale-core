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
    Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> projection);
    Task<IEnumerable<TResult>> GetByExpression<TResult>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> projection);
    Task<IEnumerable<TResult>> GetByExpression<TResult, TKey>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> projection,
        Expression<Func<TEntity, TKey>> orderByExpression,
        bool ascending = true,
        params Expression<Func<TEntity, object>>[] includes);

    Task<TResult> GetFirstByExpression<TResult>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> projection);
    Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
}
