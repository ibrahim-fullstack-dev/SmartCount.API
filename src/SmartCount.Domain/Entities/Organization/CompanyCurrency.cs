
using SmartCount.Domain.Entities.System;

namespace SmartCount.Domain.Entities.Organization;

public class CompanyCurrency
{
    public int CompanyId { get; private set; }

    public int CurrencyId { get; private set; }

    public Company Company { get; private set; } = null!;

    public Currency Currency { get; private set; } = null!;

    private CompanyCurrency()
    {
    }

    private CompanyCurrency(int companyId, int currencyId)
    {
        CompanyId = companyId;
        CurrencyId = currencyId;
    }

    public static CompanyCurrency Create(int companyId, int currencyId)
    {
        if (companyId <= 0)
            throw new ArgumentOutOfRangeException(nameof(companyId));

        if (currencyId <= 0)
            throw new ArgumentOutOfRangeException(nameof(currencyId));

        return new CompanyCurrency(companyId, currencyId);
    }
}
