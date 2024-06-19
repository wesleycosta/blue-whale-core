using Microsoft.EntityFrameworkCore;
using PixelHotel.Core.Domain;
using System.Linq;

namespace PixelHotel.Infra.Data;

public abstract class RepositoryBaseWithRemoved<TEntity> : RepositoryBase<TEntity> where TEntity : EntityBase
{
    protected RepositoryBaseWithRemoved(DbContext context) : base(context)
    {
    }

    protected override IQueryable<TEntity> AsQueryable
        => DbSet.AsQueryable();
}
