
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCount.Domain.Entities.System;

namespace SmartCount.Infrastructure.Persistence.Configurations;

public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        // Table
        builder.ToTable("features");

        // Primary Key
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .UseIdentityColumn();

        // Properties
        builder.Property(f => f.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(f => f.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(f => f.Description)
            .HasMaxLength(500);

        builder.Property(f => f.IsActive)
            .HasDefaultValue(true);

        // Unique Index
        builder.HasIndex(f => f.Code)
            .IsUnique()
            .HasDatabaseName("UQ_features_code");
    }
}
