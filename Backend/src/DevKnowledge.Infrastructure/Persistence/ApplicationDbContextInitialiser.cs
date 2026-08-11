using DevKnowledge.Domain.Entities;
using DevKnowledge.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevKnowledge.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // 1. Seed Default Roles
        var adminRole = new IdentityRole<Guid>("Admin");
        var userRole = new IdentityRole<Guid>("User");

        if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
        {
            await _roleManager.CreateAsync(adminRole);
        }

        if (_roleManager.Roles.All(r => r.Name != userRole.Name))
        {
            await _roleManager.CreateAsync(userRole);
        }

        // 2. Seed Default Admin User
        var adminEmail = "admin@gmail.com";
        var administrator = new ApplicationUser { UserName = adminEmail, Email = adminEmail, DisplayName = "System Admin" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Admin123!");
            if (!string.IsNullOrWhiteSpace(adminRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { adminRole.Name });
            }
        }

        // 3. Seed Default Domains
        if (!await _context.Domains.AnyAsync())
        {
            _logger.LogInformation("Seeding default domains...");
            _context.Domains.AddRange(new List<Domain.Entities.Domain>
            {
                new Domain.Entities.Domain { Name = "Backend", Description = "Kiến thức về lập trình Server", Slug = "backend" },
                new Domain.Entities.Domain { Name = "Frontend", Description = "Kiến thức về lập trình Giao diện", Slug = "frontend" },
                new Domain.Entities.Domain { Name = "DevOps", Description = "Kiến thức về Triển khai và Vận hành", Slug = "devops" }
            });

            await _context.SaveChangesAsync();
        }
    }
}
