using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Banking;

public class BankAccount : AuditableEntity
{
    public int CompanyId { get; private set; }

    public int AccountId { get; private set; }

    public string AccountNumber { get; private set; } = null!;

    public string BankName { get; private set; } = null!;

    public int CurrencyId { get; private set; }

    public string? RoutingNumber { get; private set; }

    private BankAccount() { }

    private BankAccount(int companyId,int accountId,string accountNumber,string bankName,int currencyId,string? routingNumber)
    {
        CompanyId = companyId;
        AccountId = accountId;
        AccountNumber = accountNumber.Trim();
        BankName = bankName.Trim();
        CurrencyId = currencyId;
        RoutingNumber = NormalizeRoutingNumber(routingNumber);
    }

    public static BankAccount Create(int companyId,int accountId,string accountNumber,string bankName,int currencyId,string? routingNumber = null)
    {
        return new BankAccount(companyId,accountId,accountNumber,bankName,currencyId,routingNumber);
    }

    public void UpdateDetails(string accountNumber,string bankName,int currencyId,string? routingNumber)
    {
        AccountNumber = accountNumber.Trim();
        BankName = bankName.Trim();
        CurrencyId = currencyId;
        RoutingNumber = NormalizeRoutingNumber(routingNumber);
    }

    private static string? NormalizeRoutingNumber(string? routingNumber)
    {
        return string.IsNullOrWhiteSpace(routingNumber)
            ? null
            : routingNumber.Trim();
    }
}