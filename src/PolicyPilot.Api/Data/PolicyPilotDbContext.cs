using Microsoft.EntityFrameworkCore;
using PolicyPilot.Api.Models;

namespace PolicyPilot.Api.Data;

public class PolicyPilotDbContext(DbContextOptions<PolicyPilotDbContext> options) : DbContext(options)
{
    public DbSet<Policy> Policies => Set<Policy>();
    public DbSet<Claim> Claims => Set<Claim>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Policy>(entity =>
        {
            entity.ToTable("Policies");
            entity.Property(policy => policy.PolicyNumber).HasMaxLength(32).IsRequired();
            entity.HasIndex(policy => policy.PolicyNumber).IsUnique();
            entity.Property(policy => policy.HolderName).HasMaxLength(200).IsRequired();
            entity.Property(policy => policy.Type).HasConversion<string>().HasMaxLength(20);
            entity.Property(policy => policy.Status).HasConversion<string>().HasMaxLength(20);
        });

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.ToTable("Claims");
            entity.Property(claim => claim.Description).HasMaxLength(1000).IsRequired();
            entity.Property(claim => claim.Status).HasConversion<string>().HasMaxLength(20);
            entity.HasOne(claim => claim.Policy)
                .WithMany(policy => policy.Claims)
                .HasForeignKey(claim => claim.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        PolicySeed.Apply(modelBuilder);
    }
}
