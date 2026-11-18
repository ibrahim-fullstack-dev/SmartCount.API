
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCount.Domain.Entities.System;

namespace SmartCount.Infrastructure.Persistence.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        // Table
        builder.ToTable("currencies");

        // Primary Key
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .UseIdentityColumn();

        // Properties
        builder.Property(c => c.Code)
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(c => c.NameAr)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(c => c.NameEn)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(c => c.Symbol)
            .HasMaxLength(10);

        builder.Property(c => c.DecimalPlaces)
            .HasDefaultValue((byte)2);

        builder.Property(c => c.IsActive)
            .HasDefaultValue(true);

        // Unique Index
        builder.HasIndex(c => c.Code)
            .IsUnique()
            .HasDatabaseName("UQ_currencies_code");

        // Check Constraint
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_currencies_decimal_places",
            "[decimal_places] <= 4"));
    }
}
