using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Organization;

public class Company : AuditableEntity
{
    public int TenantId { get; private set; }

    public string Name { get; private set; } = null!;

    public string Address { get; private set; } = null!;

    public int DefaultCurrencyId { get; private set; }

    public string RegistrationNumber { get; private set; } = null!;

    public string TaxNumber { get; private set; } = null!;

    private Company()
    {
    }

    private Company(int tenantId,string name,string address,int defaultCurrencyId,string registrationNumber,string taxNumber)
    {
        TenantId = tenantId;
        Name = name.Trim();
        Address = address.Trim();
        DefaultCurrencyId = defaultCurrencyId;
        RegistrationNumber = registrationNumber.Trim();
        TaxNumber = taxNumber.Trim();
    }

    public static Company Create(int tenantId,string name,string address,int defaultCurrencyId,string registrationNumber,string taxNumber)
    {
        return new Company(tenantId,name,address,defaultCurrencyId,registrationNumber,taxNumber);
    }

    public void UpdateDetails(string name,string address,int defaultCurrencyId,string registrationNumber,string taxNumber)
    {
        Name = name.Trim();
        Address = address.Trim();
        DefaultCurrencyId = defaultCurrencyId;
        RegistrationNumber = registrationNumber.Trim();
        TaxNumber = taxNumber.Trim();
    }

}