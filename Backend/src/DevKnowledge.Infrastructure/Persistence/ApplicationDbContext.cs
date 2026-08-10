using Microsoft.EntityFrameworkCore;
using DevKnowledge.Application.Common.Interfaces;
using DevKnowledge.Domain.Entities;

namespace DevKnowledge.Infrastructure.Persistence;

// Khung DbContext - áp dụng Configurations (Fluent API) bằng ApplyConfigurationsFromAssembly ở Part 3.
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Domain.Entities.Domain> Domains => Set<Domain.Entities.Domain>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Knowledge> Knowledges => Set<Knowledge>();
    public DbSet<Source> Sources => Set<Source>();
    public DbSet<KnowledgeSource> KnowledgeSources => Set<KnowledgeSource>();
    public DbSet<CodeExample> CodeExamples => Set<CodeExample>();
    public DbSet<TechTerm> TechTerms => Set<TechTerm>();
    public DbSet<LearningProgress> LearningProgresses => Set<LearningProgress>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
