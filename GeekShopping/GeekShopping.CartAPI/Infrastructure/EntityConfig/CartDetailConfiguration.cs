using GeekShopping.CartAPI.Domain.Entities;
using GeekShopping.ProductAPI.Infrastructure.EntityConfig.BaseConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekShopping.CartAPI.Infrastructure.EntityConfig;

public sealed class CartDetailConfiguration : EntitiesConfiguration<CartDetail>
{
    public override void Configure(EntityTypeBuilder<CartDetail> builder)
    {
        builder.ToTable("cartdetails");

        builder.Property(x => x.CartHeaderId).HasColumnName("cartheader_id");
        builder.Property(x => x.ProductId).HasColumnName("product_id");
        builder.Property(x => x.Count).HasColumnName("count");

        base.Configure(builder);
    }
}
