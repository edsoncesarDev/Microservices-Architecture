using GeekShopping.ProductAPI.Domain.Entities;
using GeekShopping.ProductAPI.Infrastructure.EntityConfig.BaseConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekShopping.ProductAPI.Infrastructure.EntityConfig;

public class ProductsConfiguration : EntitiesConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Price).HasColumnName("price");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Category).HasColumnName("category");
        builder.Property(x => x.ImageURL).HasColumnName("imageurl");

        base.Configure(builder);
    }
}
