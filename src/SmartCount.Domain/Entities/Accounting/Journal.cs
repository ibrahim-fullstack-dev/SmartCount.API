
using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Accounting;

public class Journal : AuditableEntity
{
    public int CompanyId { get; private set; }

    public string Code { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    private Journal()
    {
    }

    private Journal(int companyId,string code,string name)
    {
        CompanyId = companyId;
        Code = code.Trim();
        Name = name.Trim();
    }

    public static Journal Create(int companyId,string code,string name)
    {
        return new Journal(companyId, code, name);
    }

    public void UpdateDetails(string name)
    {
        Name = name.Trim();
    }

   
}
