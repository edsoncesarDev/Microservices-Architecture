using GeekShopping.IdentityUserAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GeekShopping.IdentityUserAPI.Infrastructure.Context;

public class AppUserContext : DbContext
{
    public AppUserContext(DbContextOptions<AppUserContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
