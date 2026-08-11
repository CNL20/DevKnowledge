using Microsoft.AspNetCore.Identity;

namespace DevKnowledge.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string DisplayName { get; set; } = default!;
}
