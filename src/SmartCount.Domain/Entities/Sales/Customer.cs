
using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Sales;

public class Customer : AuditableEntity
{
    public int CompanyId { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Address { get; private set; }

    public string? Email { get; private set; }

    public string? Phone { get; private set; }

    public string? TaxNumber { get; private set; }

    private Customer()
    {
    }

    private Customer(int companyId,string name,string? address,string? email,string? phone,string? taxNumber)
    {
        CompanyId = companyId;
        Name = name.Trim();
        Address = NormalizeOptional(address);
        Email = NormalizeOptional(email);
        Phone = NormalizeOptional(phone);
        TaxNumber = NormalizeOptional(taxNumber);
    }

    public static Customer Create(int companyId,string name,string? address = null,string? email = null,string? phone = null,string? taxNumber = null)
    {
        return new Customer(companyId,name,address,email,phone,taxNumber);
    }

    public void UpdateDetails(string name,string? address,string? email,string? phone,string? taxNumber)
    {
        Name = name.Trim();
        Address = NormalizeOptional(address);
        Email = NormalizeOptional(email);
        Phone = NormalizeOptional(phone);
        TaxNumber = NormalizeOptional(taxNumber);
    }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
