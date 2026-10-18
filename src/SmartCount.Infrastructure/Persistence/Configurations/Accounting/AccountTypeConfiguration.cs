using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SmartCount.Domain.Entities.Accounting;


namespace SmartCount.Infrastructure.Persistence.Configurations.Accounting;

public sealed class AccountTypeConfiguration: IEntityTypeConfiguration<AccountType>
{
    public void Configure(EntityTypeBuilder<AccountType> builder)
    {
        // Table
        builder.ToTable("account_types", table =>
        {
            table.HasCheckConstraint(
                "CK_account_types_normal_balance","[normal_balance] IN ('Debit', 'Credit')"
                );
        });

        // Primary Key
        builder.HasKey( x => x.Id );

        builder.Property( x => x.Id).HasColumnName( "id" ).UseIdentityColumn();


    }
}
