
using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Banking;

public class BankStatementLine : AuditableEntity
{
    public int BankStatementId { get; private set; }
    public decimal Amount { get; private set; }
    public string? Description { get; private set; }
    public string? ReferenceNumber { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public DateTime ValueDate { get; private set; }

    public BankStatement BankStatement { get; private set; } = null!;

    private BankStatementLine() { }

    private BankStatementLine(int bankStatementId,decimal amount,string? description,string? referenceNumber,DateTime transactionDate,DateTime valueDate)
    {
        BankStatementId = bankStatementId;
        Amount = amount;
        Description = description?.Trim();
        ReferenceNumber = referenceNumber?.Trim();
        TransactionDate = transactionDate;
        ValueDate = valueDate;
    }

    public static BankStatementLine Create(int bankStatementId,decimal amount,string? description,string? referenceNumber,DateTime transactionDate,DateTime valueDate)
    {
        Validate(
            bankStatementId,
            description,
            referenceNumber,
            transactionDate,
            valueDate);

        return new BankStatementLine(
            bankStatementId,
            amount,
            description,
            referenceNumber,
            transactionDate,
            valueDate);
    }

    public void Update(decimal amount,string? description,string? referenceNumber,DateTime transactionDate,DateTime valueDate)
    {
        Validate(
            BankStatementId,
            description,
            referenceNumber,
            transactionDate,
            valueDate);

        Amount = amount;
        Description = description?.Trim();
        ReferenceNumber = referenceNumber?.Trim();
        TransactionDate = transactionDate;
        ValueDate = valueDate;
    }

    private static void Validate(int bankStatementId,string? description,string? referenceNumber,DateTime transactionDate,DateTime valueDate)
    {
        if (bankStatementId <= 0)
            throw new ArgumentException("Invalid bank statement ID.");

        if (description?.Length > 500)
            throw new ArgumentException("Description cannot exceed 500 characters.");

        if (referenceNumber?.Length > 36)
            throw new ArgumentException("Reference number cannot exceed 36 characters.");

        if (transactionDate == default)
            throw new ArgumentException("Transaction date is required.");

        if (valueDate == default)
            throw new ArgumentException("Value date is required.");
    }
}
