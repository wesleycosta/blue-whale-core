using Microsoft.EntityFrameworkCore;
using PixelHotel.Core.Abstractions;
using PixelHotel.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PixelHotel.Infra.Data;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
{
    private readonly DbSet<TEntity> _dbSet;

    protected RepositoryBase(DbContext context)
        => _dbSet = context.Set<TEntity>();

    protected IQueryable<TEntity> AsQueryable()
        => _dbSet.AsQueryable();

    public virtual void Add(TEntity entity) =>
        _dbSet.Add(entity);

    public virtual void Update(TEntity entity) =>
        _dbSet.Update(entity);

    public virtual async Task Remove(Guid id)
    {
        var entity = await GetById(id);
        if (entity is null)
            return;

        _dbSet.Remove(entity);
    }

    public virtual async Task<TEntity> GetById(Guid id)
        => await AsQueryable().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<TResult>> GetByExpression<TResult>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> projection)
    {
        var query = AsQueryable();

        return await query
            .Where(filter)
            .Select(projection)
            .ToListAsync();
    }

    public async Task<TResult> GetFirstByExpression<TResult>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> projection)
    {
        var query = AsQueryable();

        return await query
            .Where(filter)
            .Select(projection)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        => await AsQueryable().AnyAsync(predicate);
}
