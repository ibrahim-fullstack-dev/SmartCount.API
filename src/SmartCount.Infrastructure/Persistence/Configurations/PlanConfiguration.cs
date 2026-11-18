
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCount.Domain.Entities.System;
using SmartCount.Domain.Entities.Tenancy;

namespace SmartCount.Infrastructure.Persistence.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("plans");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .UseIdentityColumn();

        builder.Property(p => p.Code)
            .HasColumnName("code")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(p => p.MonthlyPrice)
            .HasColumnName("monthly_price")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.YearlyPrice)
            .HasColumnName("yearly_price")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.CurrencyId)
            .HasColumnName("currency_id")
            .IsRequired();

        builder.Property(p => p.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);

        builder.HasIndex(p => p.Code)
            .IsUnique()
            .HasDatabaseName("UQ_plans_code");

        builder.HasIndex(p => p.CurrencyId)
            .HasDatabaseName("IX_plans_currency_id");

        builder.HasOne<Currency>()
            .WithMany()
            .HasForeignKey(p => p.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_plans_monthly_price",
                "[monthly_price] >= 0");

            t.HasCheckConstraint(
                "CK_plans_yearly_price",
                "[yearly_price] >= 0");

            t.HasCheckConstraint(
                "CK_plans_name",
                "LEN(LTRIM(RTRIM([name]))) > 0");
        });
    }
}
