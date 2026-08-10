using Microsoft.AspNetCore.Http;
using DevKnowledge.Application.Common.Interfaces;

namespace DevKnowledge.Infrastructure.Identity;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    public Guid? UserId => null;      // implement claim parsing ở Part 3
    public string? Email => null;
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}
