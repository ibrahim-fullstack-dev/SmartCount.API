
using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Banking;

public class Reconciliation : AuditableEntity
{
    public int BankStatementLineId { get; private set; }

    public int JournalLineId { get; private set; }

    public DateTime ReconciliationDate { get; private set; }

    private Reconciliation()
    {
    }

    private Reconciliation(int bankStatementLineId,int journalLineId,DateTime reconciliationDate)
    {
        BankStatementLineId = bankStatementLineId;
        JournalLineId = journalLineId;
        ReconciliationDate = reconciliationDate.Date;
    }

    public static Reconciliation Create(int bankStatementLineId,int journalLineId,DateTime reconciliationDate)
    {
        return new Reconciliation(bankStatementLineId,journalLineId,reconciliationDate);
    }
}
