
using SmartCount.Domain.Entities.Common;
using SmartCount.Domain.Enums;

namespace SmartCount.Domain.Entities.Sales;

public class Invoice : AuditableEntity
{
    public int AccountingPeriodId { get; private set; }

    public int CustomerId { get; private set; }

    public DateTime DueDate { get; private set; }

    public string InvoiceNumber { get; private set; } = null!;

    public DateTime IssueDate { get; private set; }

    public InvoiceStatus Status { get; private set; }

    private Invoice()
    {
    }

    private Invoice(int accountingPeriodId,int customerId,DateTime dueDate,string invoiceNumber,DateTime issueDate)
    {
        AccountingPeriodId = accountingPeriodId;
        CustomerId = customerId;
        DueDate = dueDate.Date;
        InvoiceNumber = invoiceNumber.Trim();
        IssueDate = issueDate.Date;
        Status = InvoiceStatus.Draft;
    }

    public static Invoice Create(int accountingPeriodId,int customerId,DateTime dueDate,string invoiceNumber,DateTime issueDate)
    {
        return new Invoice(accountingPeriodId,customerId,dueDate,invoiceNumber,issueDate);
    }

    public void UpdateDetails(DateTime dueDate,DateTime issueDate)
    {
        EnsureDraft();

        DueDate = dueDate.Date;
        IssueDate = issueDate.Date;
    }

    public void MarkAsIssued()
    {
        EnsureDraft();
        Status = InvoiceStatus.Issued;
    }

    public void MarkAsPaid()
    {
        if (Status != InvoiceStatus.Issued)
        {
            throw new InvalidOperationException(
                "Only an issued invoice can be marked as paid.");
        }

        Status = InvoiceStatus.Paid;
    }

    public void Cancel()
    {
        if (Status == InvoiceStatus.Paid)
        {
            throw new InvalidOperationException(
                "A paid invoice cannot be cancelled directly.");
        }

        if (Status == InvoiceStatus.Cancelled)
        {
            throw new InvalidOperationException(
                "Invoice is already cancelled.");
        }

        Status = InvoiceStatus.Cancelled;
    }

    private void EnsureDraft()
    {
        if (Status != InvoiceStatus.Draft)
        {
            throw new InvalidOperationException(
                "Only a draft invoice can be edited.");
        }
    }

}
