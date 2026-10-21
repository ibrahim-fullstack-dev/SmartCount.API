
using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Accounting;

public class JournalEntry : AuditableEntity
{
    public int AccountingPeriodId { get; private set; }

    public string Description { get; private set; } = null!;

    public DateTime EntryDate { get; private set; }

    public bool IsPosted { get; private set; }

    public int JournalId { get; private set; }

    public DateTime? PostingDate { get; private set; }

    public string ReferenceNumber { get; private set; } = null!;

    private JournalEntry()
    {
    }

    private JournalEntry(int accountingPeriodId,string description,DateTime entryDate,int journalId,string referenceNumber)
    {
        AccountingPeriodId = accountingPeriodId;
        Description = description.Trim();
        EntryDate = entryDate.Date;
        JournalId = journalId;
        ReferenceNumber = referenceNumber.Trim();
        IsPosted = false;
        PostingDate = null;
    }

    public static JournalEntry Create(int accountingPeriodId,string description,DateTime entryDate,int journalId,string referenceNumber)
    {
        return new JournalEntry(accountingPeriodId,description,entryDate,journalId,referenceNumber);
    }

    public void UpdateDetails(string description,DateTime entryDate,string referenceNumber)
    {
        if (IsPosted)
        {
            throw new InvalidOperationException(
                "A posted journal entry cannot be edited.");
        }

        Description = description.Trim();
        EntryDate = entryDate.Date;
        ReferenceNumber = referenceNumber.Trim();
    }

    public void Post(DateTime postingDate)
    {
        if (IsPosted)
        {
            throw new InvalidOperationException(
                "Journal entry has already been posted.");
        }

        if (postingDate.Date < EntryDate.Date)
        {
            throw new ArgumentException(
                "Posting date cannot be earlier than entry date.",
                nameof(postingDate));
        }

        IsPosted = true;
        PostingDate = postingDate.Date;
    }
}
