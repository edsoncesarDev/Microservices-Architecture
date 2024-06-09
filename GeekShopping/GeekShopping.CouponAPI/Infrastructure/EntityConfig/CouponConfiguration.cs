using GeekShopping.CouponAPI.Domain.Entities;
using GeekShopping.CouponAPI.Infrastructure.EntityConfig.BaseConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekShopping.CouponAPI.Infrastructure.EntityConfig;

public class CouponConfiguration : EntitiesConfiguration<Coupon>
{
    public override void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("coupons");

        builder.Property(x => x.CouponCode).HasColumnName("coupon_code");
        builder.Property(x => x.DiscountAmount).HasColumnName("discount_amount");

        base.Configure(builder);
    }
}
