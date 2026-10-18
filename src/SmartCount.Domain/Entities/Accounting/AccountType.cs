using SmartCount.Domain.Enums;

namespace SmartCount.Domain.Entities.Accounting;

public class AccountType
{
    public int Id { get; private set; }

    public string Name { get; private set; } = null!;

    public NormalBalance NormalBalance {  get; private set; }

    private AccountType()
    {

    }
}
