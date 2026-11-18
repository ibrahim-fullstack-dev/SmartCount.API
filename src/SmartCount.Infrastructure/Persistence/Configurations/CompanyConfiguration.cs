
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCount.Domain.Entities.Organization;
using SmartCount.Domain.Entities.System;
using SmartCount.Domain.Entities.Tenancy;

namespace SmartCount.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        // Table
        builder.ToTable("companies");

        // Primary Key
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .UseIdentityColumn();

        // Properties
        builder.Property(c => c.TenantId)
            .IsRequired();

        builder.Property(c => c.DefaultCurrencyId)
            .IsRequired();

        builder.Property(c => c.Name)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(c => c.Address)
            .HasMaxLength(250);

        builder.Property(c => c.RegistrationNumber)
            .HasMaxLength(36);

        builder.Property(c => c.TaxNumber)
            .HasMaxLength(36);

        builder.Property(c => c.IsActive)
            .HasDefaultValue(true);

        // BaseEntity Properties
        builder.Property(c => c.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(c => c.RowVersion)
            .IsRowVersion();

        // AuditableEntity Properties
        builder.Property(c => c.CreatedOn)
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(c => c.UpdatedOn)
            .HasColumnType("datetime2");

        builder.Property(c => c.DeletedOn)
            .HasColumnType("datetime2");

        // Indexes
        builder.HasIndex(c => c.TenantId)
            .HasDatabaseName("IX_companies_tenant_id");

        // Relationships
        builder.HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(c => c.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Currency>()
            .WithMany()
            .HasForeignKey(c => c.DefaultCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Check Constraints
        builder.ToTable(t => t.HasCheckConstraint(
            "CK_companies_name",
            "LEN(LTRIM(RTRIM([name]))) > 0"));
    }
}
