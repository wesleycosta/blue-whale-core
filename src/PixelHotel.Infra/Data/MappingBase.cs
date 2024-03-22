using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelHotel.Core.Domain;

namespace PixelHotel.Infra.Data;

public abstract class MappingBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
{
    protected const int MAX_LENGTH_STRING = 255;

    public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}
