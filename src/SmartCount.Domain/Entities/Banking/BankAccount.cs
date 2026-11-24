
using SmartCount.Domain.Entities.Common;
using SmartCount.Domain.Entities.Organization;
using SmartCount.Domain.Entities.System;

namespace SmartCount.Domain.Entities.Accounting;

public class BankAccount : AuditableEntity
{
    public int AccountId { get; private set; }
    public int CompanyId { get; private set; }
    public int CurrencyId { get; private set; }

    public string AccountNumber { get; private set; } = null!;
    public string BankName { get; private set; } = null!;
    public string RoutingNumber { get; private set; } = null!;

    // Navigation Properties
    public Account Account { get; private set; } = null!;
    public Company Company { get; private set; } = null!;
    public Currency Currency { get; private set; } = null!;

    private BankAccount() { }

    private BankAccount(int accountId,int companyId,int currencyId,string accountNumber,string bankName,string routingNumber)
    {
        AccountId = accountId;
        CompanyId = companyId;
        CurrencyId = currencyId;
        AccountNumber = accountNumber.Trim();
        BankName = bankName.Trim();
        RoutingNumber = routingNumber.Trim();
    }


    public static BankAccount Create(int accountId,int companyId,int currencyId,string accountNumber,string bankName,string routingNumber)
    {
        if (accountId <= 0)
            throw new ArgumentOutOfRangeException(nameof(accountId));

        if (companyId <= 0)
            throw new ArgumentOutOfRangeException(nameof(companyId));

        ValidateDetails(currencyId, accountNumber, bankName, routingNumber);

        return new BankAccount(
            accountId,
            companyId,
            currencyId,
            accountNumber,
            bankName,
            routingNumber);
    }

    public void Update(int currencyId,string accountNumber,string bankName,string routingNumber)
    {
        ValidateDetails(currencyId, accountNumber, bankName, routingNumber);

        CurrencyId = currencyId;
        AccountNumber = accountNumber.Trim();
        BankName = bankName.Trim();
        RoutingNumber = routingNumber.Trim();
    }

    private static void ValidateDetails(int currencyId,string accountNumber,string bankName,string routingNumber)
    {
        if (currencyId <= 0)
            throw new ArgumentOutOfRangeException(nameof(currencyId));

        ValidateText(accountNumber, nameof(accountNumber), 36);
        ValidateText(bankName, nameof(bankName), 80);
        ValidateText(routingNumber, nameof(routingNumber), 36);
    }

    private static void ValidateText(string value,string propertyName,int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                $"{propertyName} is required.", propertyName);

        if (value.Trim().Length > maxLength)
            throw new ArgumentException(
                $"{propertyName} cannot exceed {maxLength} characters.",
                propertyName);
    }

}
