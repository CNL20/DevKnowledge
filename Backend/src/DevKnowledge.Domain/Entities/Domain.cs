using DevKnowledge.Domain.Common;

namespace DevKnowledge.Domain.Entities;

// Khung entity "Domain" - fields chi tiết sẽ được xác nhận ở Part 3 trước khi implement.
public class Domain : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Slug { get; set; } = null!;

    public ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
