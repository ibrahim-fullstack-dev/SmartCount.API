
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCount.Domain.Entities.Tenancy;

namespace SmartCount.Infrastructure.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        // Table
        builder.ToTable("tenants");

        // Primary Key
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .UseIdentityColumn();

        // Tenant Properties
        builder.Property(t => t.Code)
            .HasColumnName("code")
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(t => t.Name)
            .HasColumnName("name")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(t => t.Domain)
            .HasColumnName("domain")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(t => t.IsSuspended)
            .HasColumnName("is_suspended")
            .HasDefaultValue(false)
            .IsRequired();

        // BaseEntity Properties
        builder.Property(t => t.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(t => t.RowVersion)
            .HasColumnName("row_version")
            .IsRowVersion();

        // AuditableEntity Properties
        builder.Property(t => t.CreatedById)
            .HasColumnName("created_by_id");

        builder.Property(t => t.CreatedOn)
            .HasColumnName("created_on")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(t => t.UpdatedById)
            .HasColumnName("updated_by_id");

        builder.Property(t => t.UpdatedOn)
            .HasColumnName("updated_on")
            .HasColumnType("datetime2");

        builder.Property(t => t.DeletedById)
            .HasColumnName("deleted_by_id");

        builder.Property(t => t.DeletedOn)
            .HasColumnName("deleted_on")
            .HasColumnType("datetime2");

        // Indexes
        builder.HasIndex(t => t.Code)
            .IsUnique()
            .HasDatabaseName("UX_tenants_code");

        builder.HasIndex(t => t.Domain)
            .IsUnique()
            .HasDatabaseName("UX_tenants_domain");

        // Check Constraints
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_tenants_code",
                "LEN(LTRIM(RTRIM([code]))) > 0");

            t.HasCheckConstraint(
                "CK_tenants_name",
                "LEN(LTRIM(RTRIM([name]))) > 0");
        });
    }
}
