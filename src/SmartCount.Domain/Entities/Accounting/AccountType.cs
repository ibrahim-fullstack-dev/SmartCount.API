using SmartCount.Domain.Enums;

namespace SmartCount.Domain.Entities.Accounting;

public class AccountType
{
    public int Id { get; private set; }

    public string Code { get; private set; } = null!;

    public string NameAr { get; private set; } = null!;

    public string NameEn { get; private set; } = null!;

    public NormalBalance NormalBalance { get; private set; }

    public FinancialStatement FinancialStatement { get; private set; }

    private AccountType() { }
}