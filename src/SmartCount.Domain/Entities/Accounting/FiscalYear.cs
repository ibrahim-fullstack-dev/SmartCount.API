
using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Accounting;

public class FiscalYear : AuditableEntity
{
    public int CompanyId { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public bool IsClosed { get; private set; }

    private FiscalYear()
    {
    }

    private FiscalYear(int companyId,DateTime startDate,DateTime endDate)
    {
        CompanyId = companyId;
        StartDate = startDate.Date;
        EndDate = endDate.Date;
        IsClosed = false;
    }

    public static FiscalYear Create(int companyId, DateTime startDate, DateTime endDate)
    {
        return new FiscalYear(companyId, startDate, endDate);
    }

    public void Close()
    {
        if (IsClosed)
        {
            throw new InvalidOperationException(
                "Fiscal year is already closed.");
        }

        IsClosed = true;
    }

    public void Reopen()
    {
        if (!IsClosed)
        {
            throw new InvalidOperationException(
                "Fiscal year is not closed.");
        }

        IsClosed = false;
    }

}
