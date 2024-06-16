using GeekShopping.OrderAPI.Domain.EntityBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekShopping.OrderAPI.Infrastructure.EntityConfig.BaseConfig;

public abstract class EntitiesConfiguration<T> : IEntityTypeConfiguration<T> where T : Base
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CreationDate).HasColumnName("creationdate");
    }
}
