using Microsoft.EntityFrameworkCore;
using Orangotango.Core.Domain;
using System.Linq;

namespace Orangotango.Infra.Data;

public abstract class RepositoryBaseWithRemoved<TEntity> : RepositoryBase<TEntity> where TEntity : EntityBase
{
    protected RepositoryBaseWithRemoved(DbContext context) : base(context)
    {
    }

    protected override IQueryable<TEntity> AsQueryable()
        => DbSet.IgnoreQueryFilters().AsQueryable();
}
