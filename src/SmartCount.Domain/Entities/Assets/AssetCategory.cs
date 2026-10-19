using SmartCount.Domain.Entities.Accounting;
using SmartCount.Domain.Entities.Common;
using SmartCount.Domain.Enums;

namespace SmartCount.Domain.Entities.Assets;

public class AssetCategory : AuditableEntity
{
    public int CompanyId { get; private set; }

    public string Name { get; private set; } = null!;

    public int AssetAccountId { get; private set; }

    public int ExpenseAccountId { get; private set; }

    public int DepreciationAccountId { get; private set; }

    public DepreciationMethod DepreciationMethod { get; private set; }

    public int UsefulLifeMonths { get; private set; }

    // Navigation props
    public Account AssetAccount { get; private set; } = null!;
    public Account ExpenseAccount { get; private set; } = null!;
    public Account DepreciationAccount { get; private set; } = null!;

    private AssetCategory() { }


     private AssetCategory(int companyId,string name,int assetAccountId,int expenseAccountId,int depreciationAccountId,DepreciationMethod depreciationMethod,int usefulLifeMonths)
    {
        CompanyId = companyId;
        Name = name.Trim();
        AssetAccountId = assetAccountId;
        ExpenseAccountId = expenseAccountId;
        DepreciationAccountId = depreciationAccountId;
        DepreciationMethod = depreciationMethod;
        UsefulLifeMonths = usefulLifeMonths;
    }

    public static AssetCategory Create(int companyId,string name,int assetAccountId,int expenseAccountId,int depreciationAccountId,DepreciationMethod depreciationMethod,int usefulLifeMonths)
    {
        return new AssetCategory(companyId,name,assetAccountId,expenseAccountId,depreciationAccountId,depreciationMethod,usefulLifeMonths);
    }

    public void Update(string name,int assetAccountId,int expenseAccountId,int depreciationAccountId,DepreciationMethod depreciationMethod,int usefulLifeMonths)
    {
        Name = name.Trim();
        AssetAccountId = assetAccountId;
        ExpenseAccountId = expenseAccountId;
        DepreciationAccountId = depreciationAccountId;
        DepreciationMethod = depreciationMethod;
        UsefulLifeMonths = usefulLifeMonths;
    }

   
}