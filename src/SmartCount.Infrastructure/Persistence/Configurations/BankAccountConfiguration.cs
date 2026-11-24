
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCount.Domain.Entities.Accounting;

namespace SmartCount.Infrastructure.Persistence.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable("bank_accounts");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .UseIdentityColumn();

        builder.Property(b => b.AccountId)
            .IsRequired();

        builder.Property(b => b.CompanyId)
            .IsRequired();

        builder.Property(b => b.CurrencyId)
            .IsRequired();

        builder.Property(b => b.AccountNumber)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(b => b.BankName)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(b => b.RoutingNumber)
            .HasMaxLength(36)
            .IsRequired();

        builder.HasOne(b => b.Account)
            .WithMany()
            .HasForeignKey(b => b.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Company)
            .WithMany()
            .HasForeignKey(b => b.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Currency)
            .WithMany()
            .HasForeignKey(b => b.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(b => b.AccountId)
            .IsUnique()
            .HasDatabaseName("UX_bank_accounts_account_id");

        builder.HasIndex(b => b.CompanyId)
            .HasDatabaseName("IX_bank_accounts_company_id");

        builder.HasIndex(b => b.CurrencyId)
            .HasDatabaseName("IX_bank_accounts_currency_id");

        builder.Property(b => b.RowVersion)
            .IsRowVersion();
    }
}
