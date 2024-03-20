using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<FeatureEntity> Features { get; set; }
    public DbSet<FeatureItemEntity> FeatureItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AddressEntity>()
            .HasOne(a => a.User)
            .WithOne(u => u.Address)
            .HasForeignKey<AddressEntity>(a => a.UserId);

        builder.Entity<UserEntity>()
            .HasOne(u => u.Address)
            .WithOne(a => a.User)
            .HasForeignKey<AddressEntity>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
