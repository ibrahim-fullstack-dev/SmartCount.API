
namespace SmartCount.Domain.Entities.Common;

public abstract class BaseEntity
{
    public int Id { get; private set; }
    public bool IsDeleted { get; private set; }
    public byte[] RowVersion { get; private set; } = new byte[0];
}
