
using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Accounting;

public class CostCenter : AuditableEntity
{
    public int CompanyId { get; private set; }

    public string Code { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    private CostCenter()
    {
    }

    private CostCenter(int companyId, string code, string name)
    {
        CompanyId = companyId;
        Code = code.Trim();
        Name = name.Trim();
    }

    public static CostCenter Create(int companyId,string code,string name)
    {
        return new CostCenter(companyId, code, name);
    }

    public void UpdateName(string name)
    {
        Name = name.Trim();
    }

}
