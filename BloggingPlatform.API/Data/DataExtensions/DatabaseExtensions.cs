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
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Top 10 programming languages in 2024",
                Content = "Discover the most popular programming languages and their use cases.",
                Category = "Technology",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "How to stay productive while working remotely",
                Content = "Tips and tricks for maintaining productivity when working from home.",
                Category = "Productivity",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "The future of AI in software development",
                Content = "Exploring how artificial intelligence is shaping the future of coding.",
                Category = "AI",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Understanding microservices architecture",
                Content = "A deep dive into the benefits and challenges of microservices.",
                Category = "Architecture",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Getting started with Kubernetes",
                Content = "A beginner's guide to deploying and managing containerized applications with Kubernetes.",
                Category = "DevOps",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
            ,
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Docker fundamentals for developers",
                Content = "Learn how to containerize applications using Docker and best practices for images.",
                Category = "DevOps",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Implementing CI/CD pipelines",
                Content = "A practical guide to setting up continuous integration and continuous deployment pipelines.",
                Category = "DevOps",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Unit testing strategies in .NET",
                Content = "Effective approaches for writing maintainable unit tests using xUnit and mocking frameworks.",
                Category = "Testing",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Building interactive UIs with Blazor",
                Content = "An introduction to Blazor for creating client-side web UIs with C# instead of JavaScript.",
                Category = "Frontend",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "EF Core migrations demystified",
                Content = "How to manage database schema changes safely using Entity Framework Core migrations.",
                Category = "Database",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Security best practices for web apps",
                Content = "Essential security measures to protect web applications from common threats.",
                Category = "Security",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Performance tuning .NET applications",
                Content = "Tips and tools for diagnosing and improving performance in .NET applications.",
                Category = "Performance",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Principles of clean code",
                Content = "Guidelines and practices to write readable, maintainable, and testable code.",
                Category = "Software Engineering",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Designing cloud-native applications on Azure",
                Content = "Architectural patterns and services to build scalable cloud-native solutions on Azure.",
                Category = "Cloud",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Observability: logging, metrics, and tracing",
                Content = "How to gain actionable insights into system behavior using observability practices.",
                Category = "Operations",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        context.Posts.AddRange(defaultPosts);
        context.SaveChanges();
    }
}
