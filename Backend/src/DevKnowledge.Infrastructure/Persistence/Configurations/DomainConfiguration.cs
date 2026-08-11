using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevKnowledge.Infrastructure.Persistence.Configurations;

public class DomainConfiguration : IEntityTypeConfiguration<Domain.Entities.Domain>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Domain> builder)
    {
        builder.ToTable("domains");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Description)
            .HasMaxLength(500);

        builder.Property(d => d.Slug)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(d => d.Name).IsUnique();
        builder.HasIndex(d => d.Slug).IsUnique();
        
        // Navigation: 1 Domain has many Topics
        builder.HasMany(d => d.Topics)
            .WithOne(t => t.Domain)
            .HasForeignKey(t => t.DomainId)
            .OnDelete(DeleteBehavior.Restrict); // Chống xóa domain nếu vẫn còn topic bên trong
    }
}
