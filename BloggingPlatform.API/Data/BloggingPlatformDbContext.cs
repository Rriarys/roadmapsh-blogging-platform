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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BloggingPlatformDbContext).Assembly);
    }
}
