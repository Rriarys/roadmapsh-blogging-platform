using BloggingPlatform.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.API.Data.DataExtensions;

public static class DatabaseExtensions
{
    public static void ApplyMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BloggingPlatformDbContext>();
        dbContext.Database.Migrate();

        SeedData(dbContext);
    }

    private static void SeedData(BloggingPlatformDbContext context)
    {
        if (context.Posts.Any())
        {
            return; // Database has already been seeded
        }

        var defaultPosts = new List<Post>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Welcome to our new Platform",
                Content = "This is the very first post on this amazing blogging platform.",
                Category = "News",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Why learning .NET is awesome",
                Content = "C# and .NET provide incredible performance and great developer experience.",
                Category = "Education",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        context.Posts.AddRange(defaultPosts);
        context.SaveChanges();
    }
}
