
using SmartCount.Domain.Entities.Common;
using SmartCount.Domain.Enums;

namespace SmartCount.Domain.Entities.Banking;

public class BankStatementLine : AuditableEntity
{
    public int BankStatementId { get; private set; }

    public decimal Amount { get; private set; }

    public BankTransactionType TransactionType { get; private set; }

    public string Description { get; private set; } = null!;

    public string ReferenceNumber { get; private set; } = null!;

    public DateTime TransactionDate { get; private set; }

    public DateTime ValueDate { get; private set; }

    private BankStatementLine()
    {
    }

    private BankStatementLine(int bankStatementId,decimal amount,BankTransactionType transactionType,string description,string referenceNumber,DateTime transactionDate,DateTime valueDate)
    {
        BankStatementId = bankStatementId;
        Amount = amount;
        TransactionType = transactionType;
        Description = description.Trim();
        ReferenceNumber = referenceNumber.Trim();
        TransactionDate = transactionDate.Date;
        ValueDate = valueDate.Date;
    }

    public static BankStatementLine Create(int bankStatementId,decimal amount,BankTransactionType transactionType,string description,string referenceNumber,DateTime transactionDate,DateTime valueDate)
    {
        return new BankStatementLine(bankStatementId,amount,transactionType,description,referenceNumber,transactionDate,valueDate);
    }

}
