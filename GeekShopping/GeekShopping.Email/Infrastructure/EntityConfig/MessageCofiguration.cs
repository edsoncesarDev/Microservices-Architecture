
using GeekShopping.Email.Domain.Entities;
using GeekShopping.Email.Infrastructure.EntityConfig.BaseConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekShopping.Email.Infrastructure.EntityConfig;

public class MessageCofiguration : EntitiesConfiguration<Message>
{
    public override void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("emails");

        builder.Property(x => x.EmailAddress).HasColumnName("emailaddress");
        builder.Property(x => x.Log).HasColumnName("log");


        base.Configure(builder);
    }
}
