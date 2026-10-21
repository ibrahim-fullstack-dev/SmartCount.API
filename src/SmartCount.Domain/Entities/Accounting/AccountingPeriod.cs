
using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Accounting;

public class AccountingPeriod : AuditableEntity
{
    public int FiscalYearId { get; private set; }

    public int PeriodNumber { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public bool IsClosed { get; private set; }

    private AccountingPeriod()
    {
    }

    private AccountingPeriod(int fiscalYearId,int periodNumber,DateTime startDate,DateTime endDate)
    {
        FiscalYearId = fiscalYearId;
        PeriodNumber = periodNumber;
        StartDate = startDate.Date;
        EndDate = endDate.Date;
        IsClosed = false;
    }

    public static AccountingPeriod Create(int fiscalYearId,int periodNumber,DateTime startDate,DateTime endDate)
    {
        return new AccountingPeriod(fiscalYearId,periodNumber,startDate,endDate);
    }

    public void Close()
    {
        if (IsClosed)
        {
            throw new InvalidOperationException(
                "Accounting period is already closed.");
        }

        IsClosed = true;
    }

    public void Reopen()
    {
        if (!IsClosed)
        {
            throw new InvalidOperationException(
                "Accounting period is not closed.");
        }

        IsClosed = false;
    }
}
