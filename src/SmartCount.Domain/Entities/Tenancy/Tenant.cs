
using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Tenancy;

public class Tenant : AuditableEntity
{
    public string Code { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string Domain { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public bool IsSuspended { get; private set; }

    private Tenant()
    {
    }

    private Tenant(string code, string name, string domain)
    {
        Code = ValidateRequiredText(code, nameof(Code), 36);
        Name = ValidateRequiredText(name, nameof(Name), 120);
        Domain = NormalizeDomain(domain);

        IsActive = true;
        IsSuspended = false;
    }

    public static Tenant Create(string code, string name, string domain)
    {
        return new Tenant(code, name, domain);
    }

    public void UpdateDetails(string name, string domain)
    {
        // Validate everything before changing the entity
        var validatedName = ValidateRequiredText(name, nameof(Name), 120);
        var validatedDomain = NormalizeDomain(domain);

        Name = validatedName;
        Domain = validatedDomain;
    }

    public void Activate()
    {
        if (IsActive)
        {
            throw new InvalidOperationException(
                "Tenant is already active.");
        }

        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException(
                "Tenant is already inactive.");
        }

        IsActive = false;
        IsSuspended = false;
    }

    public void Suspend()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException(
                "An inactive tenant cannot be suspended.");
        }

        if (IsSuspended)
        {
            throw new InvalidOperationException(
                "Tenant is already suspended.");
        }

        IsSuspended = true;
    }

    public void Unsuspend()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException(
                "An inactive tenant cannot be unsuspended.");
        }

        if (!IsSuspended)
        {
            throw new InvalidOperationException(
                "Tenant is not suspended.");
        }

        IsSuspended = false;
    }


    private static string ValidateRequiredText(string? value,string propertyName,int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                $"{propertyName} cannot be empty.",
                propertyName);
        }

        var normalized = value.Trim();

        if (normalized.Length > maxLength)
        {
            throw new ArgumentException(
                $"{propertyName} cannot exceed {maxLength} characters.",
                propertyName);
        }

        return normalized;
    }

    private static string NormalizeDomain(string? domain)
    {
        var normalized = ValidateRequiredText(
            domain,
            nameof(Domain),
            255).ToLowerInvariant();

        if (Uri.CheckHostName(normalized) != UriHostNameType.Dns)
        {
            throw new ArgumentException(
                "Domain must be a valid DNS hostname.",
                nameof(Domain));
        }

        return normalized;
    }
}
