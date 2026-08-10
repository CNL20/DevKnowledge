using DevKnowledge.Domain.Common;

namespace DevKnowledge.Domain.Entities;

// Khung entity - không chứa business logic, sẽ được hoàn thiện ở Part 3 (Feature: Authentication)
public class User : BaseEntity
{
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string DisplayName { get; private set; } = default!;
    // Role, RefreshTokens, LearningProgress... -> implement ở Part 3
}
