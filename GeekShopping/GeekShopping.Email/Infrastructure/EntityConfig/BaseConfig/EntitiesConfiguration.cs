using GeekShopping.Email.Domain.EntityBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekShopping.Email.Infrastructure.EntityConfig.BaseConfig;

public abstract class EntitiesConfiguration<T> : IEntityTypeConfiguration<T> where T : Base
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CreationDate).HasColumnName("creationdate");
    }
}
