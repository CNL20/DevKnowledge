using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DevKnowledge.Domain.Entities;

namespace DevKnowledge.Infrastructure.Persistence.Configurations;

public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.ToTable("topics");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(t => t.Description)
            .HasMaxLength(1000);

        builder.Property(t => t.Slug)
            .IsRequired()
            .HasMaxLength(200);

        // Name của Topic phải duy nhất TRONG CÙNG MỘT DOMAIN
        builder.HasIndex(t => new { t.DomainId, t.Name }).IsUnique();
        builder.HasIndex(t => new { t.DomainId, t.Slug }).IsUnique();
    }
}
