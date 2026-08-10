namespace DevKnowledge.Application.Common.Interfaces;

// Abstraction cho AI Knowledge Synthesis pipeline (implement ở Infrastructure.AI, feature detail ở Part 3).
public interface IAIKnowledgeService
{
    Task<string> SummarizeAsync(string sourceContent, CancellationToken cancellationToken);
    Task<string> ExplainAsync(string topic, string context, CancellationToken cancellationToken);
}
