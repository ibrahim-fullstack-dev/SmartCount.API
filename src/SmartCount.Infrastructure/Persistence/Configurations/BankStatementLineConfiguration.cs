
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCount.Domain.Entities.Banking;

namespace SmartCount.Infrastructure.Persistence.Configurations.Accounting;

public class BankStatementLineConfiguration
    : IEntityTypeConfiguration<BankStatementLine>
{
    public void Configure(EntityTypeBuilder<BankStatementLine> builder)
    {
        builder.ToTable("bank_statement_lines");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn();

        builder.Property(x => x.BankStatementId)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(14, 2)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.ReferenceNumber)
            .HasMaxLength(36);

        builder.Property(x => x.TransactionDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.ValueDate)
            .HasColumnType("date")
            .IsRequired();

        builder.HasOne<BankStatement>()
            .WithMany()
            .HasForeignKey(x => x.BankStatementId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(x => x.RowVersion)
            .IsRowVersion();
    }
}
