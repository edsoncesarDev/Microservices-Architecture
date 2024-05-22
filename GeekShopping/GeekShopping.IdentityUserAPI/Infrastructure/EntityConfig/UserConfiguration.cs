using GeekShopping.IdentityUserAPI.Domain;
using GeekShopping.IdentityUserAPI.Infrastructure.EntityConfig.BaseConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekShopping.IdentityUserAPI.Infrastructure.EntityConfig;

public class UserConfiguration : EntitiesConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.Password).HasColumnName("password");
        builder.Property(x => x.Role).HasColumnName("role");

        base.Configure(builder);
    }
}
