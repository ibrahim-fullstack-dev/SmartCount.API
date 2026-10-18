namespace SmartCount.Domain.Entities.Common;

public abstract class AuditableEntity: BaseEntity
{
    public int? CreatedById { get; private set; }

    public DateTime? CreatedOn { get; private set; }

    public int? UpdatedById { get; private set; }

    public DateTime? UpdatedOn { get; private set; }

    public int? DeletedById { get; private set; }

    public DateTime? DeletedOn { get; private set; }
}
