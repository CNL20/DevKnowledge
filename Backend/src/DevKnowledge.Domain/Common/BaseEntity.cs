namespace DevKnowledge.Domain.Common;

// Base entity: mọi entity trong Domain kế thừa từ đây để có audit fields chuẩn hóa.
public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAtUtc { get; protected set; }
    public DateTime? UpdatedAtUtc { get; protected set; }
    public string? CreatedBy { get; protected set; }
    public string? UpdatedBy { get; protected set; }
    public bool IsDeleted { get; protected set; } // soft delete
}
