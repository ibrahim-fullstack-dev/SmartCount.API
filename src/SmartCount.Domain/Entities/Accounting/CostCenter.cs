
using SmartCount.Domain.Entities.Common;
using SmartCount.Domain.Entities.Organization;

namespace SmartCount.Domain.Entities.Accounting;

public class CostCenter : AuditableEntity
{
    public int CompanyId { get; private set; }
    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;

    public Company Company { get; private set; } = null!;

    private CostCenter() { }

    private CostCenter(int companyId, string code, string name)
    {
        CompanyId = companyId;
        Code = code.Trim();
        Name = name.Trim();
    }

    public static CostCenter Create(int companyId, string code, string name)
    {
        Validate(companyId, code, name);

        return new CostCenter(companyId, code, name);
    }

    public void Update(string code, string name)
    {
        Validate(CompanyId, code, name);

        Code = code.Trim();
        Name = name.Trim();
    }

    private static void Validate(int companyId, string code, string name)
    {
        if (companyId <= 0)
            throw new ArgumentException("Invalid company ID.");

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Cost center code is required.");

        if (code.Trim().Length > 9)
            throw new ArgumentException("Cost center code cannot exceed 9 characters.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Cost center name is required.");

        if (name.Trim().Length > 120)
            throw new ArgumentException("Cost center name cannot exceed 120 characters.");
    }
}
