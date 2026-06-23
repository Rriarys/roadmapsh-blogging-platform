using BloggingPlatform.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.API.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Post> builder)
    {
        builder.Property(p => p.Title).IsRequired().HasMaxLength(150);
        builder.Property(p => p.Content).IsRequired().HasMaxLength(10000);
        builder.Property(p => p.Category).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Tags).HasMaxLength(300);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();
    }
}
