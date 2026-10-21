
using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Banking;

public class BankStatement : AuditableEntity
{
    public int BankAccountId { get; private set; }

    public decimal OpeningBalance { get; private set; }

    public decimal ClosingBalance { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public string StatementNumber { get; private set; } = null!;

    private BankStatement()
    {
    }

    private BankStatement(int bankAccountId,decimal openingBalance,decimal closingBalance,DateTime startDate,DateTime endDate,string statementNumber)
    {
        BankAccountId = bankAccountId;
        OpeningBalance = openingBalance;
        ClosingBalance = closingBalance;
        StartDate = startDate.Date;
        EndDate = endDate.Date;
        StatementNumber = statementNumber.Trim();
    }

    public static BankStatement Create(int bankAccountId,decimal openingBalance,decimal closingBalance,DateTime startDate,DateTime endDate,string statementNumber)
    {
        return new BankStatement(bankAccountId,openingBalance,closingBalance,startDate,endDate,statementNumber);
    }

}
