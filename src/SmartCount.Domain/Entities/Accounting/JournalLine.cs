
using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Accounting;

public class JournalLine : AuditableEntity
{
    public int JournalEntryId { get; private set; }

    public int AccountId { get; private set; }

    public int? CostCenterId { get; private set; }

    public decimal Debit { get; private set; }

    public decimal Credit { get; private set; }

    public string Description { get; private set; } = null!;

    public int? ProjectId { get; private set; }

    public int? TaxRateId { get; private set; }

    private JournalLine()
    {
    }

    private JournalLine(int journalEntryId, int accountId, decimal debit, decimal credit, string description, int? costCenterId, int? projectId, int? taxRateId)
    {
        JournalEntryId = journalEntryId;
        AccountId = accountId;
        Debit = debit;
        Credit = credit;
        Description = description.Trim();
        CostCenterId = costCenterId;
        ProjectId = projectId;
        TaxRateId = taxRateId;
    }

    public static JournalLine Create(int journalEntryId, int accountId, decimal debit, decimal credit, string description, int? costCenterId = null, int? projectId = null, int? taxRateId = null)
    {
        return new JournalLine(journalEntryId,accountId,debit,credit,description,costCenterId,projectId,taxRateId);
    }

    public void UpdateDetails(decimal debit, decimal credit, string description, int? costCenterId = null, int? projectId = null, int? taxRateId = null)
    {
        Debit = debit;
        Credit = credit;
        Description = description.Trim();
        CostCenterId = costCenterId;
        ProjectId = projectId;
        TaxRateId = taxRateId;
    }

}
