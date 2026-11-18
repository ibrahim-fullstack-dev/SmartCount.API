
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCount.Domain.Entities.Organization;

namespace SmartCount.Infrastructure.Persistence.Configurations;

public class CompanyCurrencyConfiguration
    : IEntityTypeConfiguration<CompanyCurrency>
{
    public void Configure(EntityTypeBuilder<CompanyCurrency> builder)
    {
        builder.ToTable("company_currencies");

        // Composite Primary Key
        builder.HasKey(cc => new { cc.CompanyId, cc.CurrencyId })
            .HasName("PK_company_currencies");

        // Company Relationship
        builder.HasOne(cc => cc.Company)
            .WithMany()
            .HasForeignKey(cc => cc.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Currency Relationship
        builder.HasOne(cc => cc.Currency)
            .WithMany()
            .HasForeignKey(cc => cc.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
