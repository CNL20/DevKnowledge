using DevKnowledge.Domain.Common;

namespace DevKnowledge.Domain.Entities;

// Khung entity "Topic" - fields chi tiết sẽ được xác nhận ở Part 3 trước khi implement.
public class Topic : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Slug { get; set; } = null!;

    public Guid DomainId { get; set; }
    public Domain Domain { get; set; } = null!;
}
