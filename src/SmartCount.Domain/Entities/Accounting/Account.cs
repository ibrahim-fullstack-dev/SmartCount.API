using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Accounting;

public class Account : AuditableEntity
{
    public int CompanyId { get; private set; }

    public int AccountGroupId { get; private set; }

    public string Code { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public AccountGroup AccountGroup { get; private set; } = null!;

    private Account() { }

    private Account(int companyId,int accountGroupId,string code,string name,string? description)
    {
        CompanyId = companyId;
        AccountGroupId = accountGroupId;
        Code = code;
        Name = name;
        Description = description;
        IsActive = true;
    }

    public static Account Create(int companyId,int accountGroupId,string code,string name,string? description = null)
    {
        return new Account(
            companyId,
            accountGroupId,
            code.Trim(),
            name.Trim(),
            string.IsNullOrWhiteSpace(description)
                ? null
                : description.Trim());
    }

    public void UpdateDetails(string name, string? description)
    {

        Name = name.Trim();
        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim();
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

}