
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCount.Domain.Entities.Tenancy;
using SmartCount.Domain.Entities.System;

namespace SmartCount.Infrastructure.Persistence.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("subscriptions");

        // Primary Key
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .UseIdentityColumn();

        // Relationships
        builder.Property(s => s.TenantId)
            .IsRequired();

        builder.Property(s => s.PlanId)
            .IsRequired();

        builder.Property(s => s.CurrencyId)
            .IsRequired();

        builder.HasOne(s => s.Tenant)
            .WithMany()
            .HasForeignKey(s => s.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Plan)
            .WithMany()
            .HasForeignKey(s => s.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Currency)
            .WithMany()
            .HasForeignKey(s => s.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Subscription Details
        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(12)
            .IsRequired();

        builder.Property(s => s.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(s => s.BillingCycle)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        // Subscription Dates
        builder.Property(s => s.StartDate)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(s => s.CurrentPeriodStart)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(s => s.CurrentPeriodEnd)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(s => s.TrialEndsOn)
            .HasColumnType("datetime2");

        // Cancellation
        builder.Property(s => s.CancelAtPeriodEnd)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(s => s.CancelledOn)
            .HasColumnType("datetime2");

        // Indexes
        builder.HasIndex(s => s.TenantId)
            .HasDatabaseName("IX_subscriptions_tenant_id");

        builder.HasIndex(s => s.PlanId)
            .HasDatabaseName("IX_subscriptions_plan_id");

        builder.HasIndex(s => s.CurrentPeriodEnd)
            .HasDatabaseName("IX_subscriptions_current_period_end");

        // Check Constraints
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_subscriptions_price",
                "[price] >= 0");

            t.HasCheckConstraint(
                "CK_subscriptions_period",
                "[current_period_end] > [current_period_start]");

            t.HasCheckConstraint(
                "CK_subscriptions_period_start",
                "[current_period_start] >= [start_date]");

            t.HasCheckConstraint(
                "CK_subscriptions_trial_end",
                "[trial_ends_on] IS NULL OR [trial_ends_on] > [start_date]");
        });
    }
}
