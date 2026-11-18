
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCount.Domain.Entities.Tenancy;

namespace SmartCount.Infrastructure.Persistence.Configurations;

public class PlanFeatureConfiguration : IEntityTypeConfiguration<PlanFeature>
{
    public void Configure(EntityTypeBuilder<PlanFeature> builder)
    {
        builder.ToTable("plan_features");

        // Primary Key
        builder.HasKey(pf => pf.Id);

        builder.Property(pf => pf.Id)
            .UseIdentityColumn();

        // Properties
        builder.Property(pf => pf.PlanId)
            .IsRequired();

        builder.Property(pf => pf.FeatureId)
            .IsRequired();

        builder.Property(pf => pf.IsEnabled)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(pf => pf.LimitValue);

        // Plan Relationship
        builder.HasOne(pf => pf.Plan)
            .WithMany()
            .HasForeignKey(pf => pf.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        // Feature Relationship
        builder.HasOne(pf => pf.Feature)
            .WithMany()
            .HasForeignKey(pf => pf.FeatureId)
            .OnDelete(DeleteBehavior.Restrict);

        // Unique Index
        builder.HasIndex(pf => new { pf.PlanId, pf.FeatureId })
            .IsUnique()
            .HasDatabaseName("UQ_plan_features");

        // Check Constraint
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_plan_features_limit",
            "[limit_value] IS NULL OR [limit_value] >= 0"));
    }
}
