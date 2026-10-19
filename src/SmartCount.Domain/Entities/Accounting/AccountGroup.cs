using SmartCount.Domain.Entities.Common;

namespace SmartCount.Domain.Entities.Accounting;

public class AccountGroup: AuditableEntity
{
    public int CompanyId { get; private set; }

    public int AccountTypeId { get; private set; }

    public string Name { get; private set; } = null!;

    public string Code { get; private set; } = null!;

    public int? ParentGroupId { get; private set; }

    private AccountGroup()
    {
    }

    private AccountGroup(int companyId,int accountTypeId,int? parentGroupId,string code,string name)
    {
        CompanyId = companyId;
        AccountTypeId = accountTypeId;
        ParentGroupId = parentGroupId;
        Name = name;
        Code = code;
    }

    public static AccountGroup Create(int companyId,int accountTypeId,int? parentGroupId,string code,string name)
    {
        return new AccountGroup(
           companyId,
           accountTypeId,
           parentGroupId,
           name.Trim(),
           code.Trim());
    }

    public void Update(int accountTypeId,int? parentGroupId,string name)
    {

        AccountTypeId = accountTypeId;
        ParentGroupId = parentGroupId;
        Name = name.Trim();
    }

}
