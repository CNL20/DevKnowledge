using DevKnowledge.Application.Common.Interfaces;

namespace DevKnowledge.Infrastructure.AI;

// Khung implementation - sẽ gọi LLM provider thật ở Part 3 (AI Pipeline feature).
public class AIKnowledgeService : IAIKnowledgeService
{
    public Task<string> SummarizeAsync(string sourceContent, CancellationToken cancellationToken)
        => throw new NotImplementedException("Implement ở Part 3 - AI Pipeline");

    public Task<string> ExplainAsync(string topic, string context, CancellationToken cancellationToken)
        => throw new NotImplementedException("Implement ở Part 3 - AI Pipeline");
}
