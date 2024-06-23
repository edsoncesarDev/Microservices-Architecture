using GeekShopping.OrderAPI.Domain.Entities;
using GeekShopping.OrderAPI.Infrastructure.EntityConfig.BaseConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekShopping.OrderAPI.Infrastructure.EntityConfig;

public sealed class OrderDetailConfiguration : EntitiesConfiguration<OrderDetail>
{
    public override void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("orderdetails");

        builder.Property(x => x.OrderHeaderId).HasColumnName("order_header_id");
        builder.Property(x => x.ProductId).HasColumnName("product_id");
        builder.Property(x => x.Count).HasColumnName("count");
        builder.Property(x => x.ProductName).HasColumnName("product_name");
        builder.Property(x => x.Price).HasColumnName("price");

        base.Configure(builder);
    }
}
