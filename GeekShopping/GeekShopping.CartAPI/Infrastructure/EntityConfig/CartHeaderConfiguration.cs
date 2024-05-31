using GeekShopping.CartAPI.Domain.Entities;
using GeekShopping.ProductAPI.Infrastructure.EntityConfig.BaseConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekShopping.CartAPI.Infrastructure.EntityConfig;

public class CartHeaderConfiguration : EntitiesConfiguration<CartHeader>
{
    public override void Configure(EntityTypeBuilder<CartHeader> builder)
    {
        builder.ToTable("cartheaders");

        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.CouponCode).HasColumnName("cupon_code");

        base.Configure(builder);
    }
}
