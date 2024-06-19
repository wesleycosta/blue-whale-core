using Microsoft.EntityFrameworkCore;
using PixelHotel.Core.Abstractions;
using PixelHotel.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PixelHotel.Infra.Data;

public abstract class RepositoryBase<TEntity>(DbContext context) : IRepositoryBase<TEntity> where TEntity : EntityBase
{
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    protected virtual IQueryable<TEntity> AsQueryable => DbSet.AsQueryable()
            .Where(p => !p.Removed);

    public virtual void Add(TEntity entity) =>
        DbSet.Add(entity);

    public virtual void Update(TEntity entity) =>
        DbSet.Update(entity);

    public virtual async Task HardDelete(Guid id)
    {
        var entity = await GetById(id);
        if (entity is null)
            return;

        DbSet.Remove(entity);
    }

    public virtual async Task SoftDelete(Guid id)
    {
        var entity = await GetById(id);
        if (entity is null)
            return;

        entity.Remove();
        Update(entity);
    }

    public virtual async Task<TEntity> GetById(Guid id)
        => await AsQueryable.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<TResult>> GetByExpression<TResult>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> projection)
    {
        var query = AsQueryable;

        return await query
            .Where(filter)
            .Select(projection)
            .ToListAsync();
    }

    public async Task<TResult> GetFirstByExpression<TResult>(Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TResult>> projection)
    {
        var query = AsQueryable;

        return await query
            .Where(filter)
            .Select(projection)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        => await AsQueryable.AnyAsync(predicate);
}
