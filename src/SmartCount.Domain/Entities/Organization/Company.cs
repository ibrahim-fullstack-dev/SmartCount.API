using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Organization;

public class Company : AuditableEntity
{
    public int TenantId { get; private set; }

    public int DefaultCurrencyId { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Address { get; private set; }

    public string? RegistrationNumber { get; private set; }

    public string? TaxNumber { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsActive { get; private set; }

    private Company()
    {
    }

    private Company(int tenantId,string name,int defaultCurrencyId,string? address,string? registrationNumber,string? taxNumber)
    {
        TenantId = tenantId;
        DefaultCurrencyId = defaultCurrencyId;
        Name = NormalizeRequired(name, 80);
        Address = NormalizeOptional(address, 250);
        RegistrationNumber = NormalizeOptional(registrationNumber, 36);
        TaxNumber = NormalizeOptional(taxNumber, 36);
        IsActive = true;
    }

    public static Company Create(int tenantId,string name,int defaultCurrencyId,string? address,string? registrationNumber,string? taxNumber)
    {
        if (tenantId <= 0)
            throw new ArgumentException("Tenant ID must be greater than zero.");

        if (defaultCurrencyId <= 0)
            throw new ArgumentException("Currency ID must be greater than zero.");

        return new Company(tenantId,name,defaultCurrencyId,address,registrationNumber,taxNumber);
    }

    public void UpdateDetails(string name,string? address,string? registrationNumber,string? taxNumber)
    {
        Name = NormalizeRequired(name, 80);
        Address = NormalizeOptional(address, 250);
        RegistrationNumber = NormalizeOptional(registrationNumber, 36);
        TaxNumber = NormalizeOptional(taxNumber, 36);
    }

    public void ChangeDefaultCurrency(int currencyId)
    {
        if (currencyId <= 0)
            throw new ArgumentException("Currency ID must be greater than zero.");

        DefaultCurrencyId = currencyId;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    private static string NormalizeRequired(string value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value is required.");

        value = value.Trim();

        if (value.Length > maxLength)
            throw new ArgumentException(
                $"Value cannot exceed {maxLength} characters.");

        return value;
    }

    private static string? NormalizeOptional(string? value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        value = value.Trim();

        if (value.Length > maxLength)
            throw new ArgumentException(
                $"Value cannot exceed {maxLength} characters.");

        return value;
    }
}