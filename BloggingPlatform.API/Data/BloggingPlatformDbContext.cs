using BloggingPlatform.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.API.Data;

public class BloggingPlatformDbContext : DbContext
{
    public BloggingPlatformDbContext(DbContextOptions<BloggingPlatformDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure the Post entity
        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired().HasMaxLength(2000);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
        });
    }
}
