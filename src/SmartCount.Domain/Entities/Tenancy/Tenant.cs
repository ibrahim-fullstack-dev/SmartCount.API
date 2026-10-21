using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Tenancy;

public class Tenant : AuditableEntity
{
    public string Code { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string? Domain { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsSuspended { get; private set; }

    private Tenant()
    {
    }

    private Tenant(string code,string name,string? domain)
    {
        Code = code.Trim();
        Name = name.Trim();
        Domain = NormalizeDomain(domain);

        IsActive = true;
        IsSuspended = false;
    }

    public static Tenant Create(string code,string name,string? domain = null)
    {
        return new Tenant(code, name, domain);
    }

    public void UpdateDetails(string name,string? domain)
    {
        Name = name.Trim();
        Domain = NormalizeDomain(domain);
    }

    public void Suspend()
    {
        IsSuspended = true;
    }

    public void Reactivate()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException(
                "An inactive tenant cannot be reactivated through this operation.");
        }

        IsSuspended = false;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private static string? NormalizeDomain(string? domain)
    {
        return string.IsNullOrWhiteSpace(domain)
            ? null
            : domain.Trim().ToLowerInvariant();
    }
}