using GeekShopping.OrderAPI.Domain.Entities;
using GeekShopping.OrderAPI.Infrastructure.EntityConfig.BaseConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekShopping.OrderAPI.Infrastructure.EntityConfig;

public sealed class OrderHeaderConfiguration : EntitiesConfiguration<OrderHeader>
{
    public override void Configure(EntityTypeBuilder<OrderHeader> builder)
    {
        builder.ToTable("orderheaders");

        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.CouponCode).HasColumnName("coupon_code");
        builder.Property(x => x.PurchaseAmount).HasColumnName("purchase_amount");
        builder.Property(x => x.DiscountAmount).HasColumnName("discount_amount");
        builder.Property(x => x.FirstName).HasColumnName("firstname");
        builder.Property(x => x.LastName).HasColumnName("lastname");
        builder.Property(x => x.OrderTime).HasColumnName("ordertime");
        builder.Property(x => x.Phone).HasColumnName("phone");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.CardNumber).HasColumnName("card_number");
        builder.Property(x => x.CVV).HasColumnName("cvv");
        builder.Property(x => x.ExpiryMonthYear).HasColumnName("expiry_month_year");
        builder.Property(x => x.CartTotalItens).HasColumnName("cart_total_itens");
        builder.Property(x => x.PaymentStatus).HasColumnName("payment_status");

        base.Configure(builder);
    }
}
