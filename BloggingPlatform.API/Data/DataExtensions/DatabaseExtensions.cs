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
                Tags = new List<string> { "announcement", "welcome" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Why learning .NET is awesome",
                Content = "C# and .NET provide incredible performance and great developer experience.",
                Category = "Education",
                Tags = new List<string> { "dotnet", "csharp", "learning" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Top 10 programming languages in 2024",
                Content = "Discover the most popular programming languages and their use cases.",
                Category = "Technology",
                Tags = new List<string> { "programming", "trends", "2024" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "How to stay productive while working remotely",
                Content = "Tips and tricks for maintaining productivity when working from home.",
                Category = "Productivity",
                Tags = new List<string> { "productivity", "remote", "tips" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "The future of AI in software development",
                Content = "Exploring how artificial intelligence is shaping the future of coding.",
                Category = "AI",
                Tags = new List<string> { "ai", "ml", "software" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Understanding microservices architecture",
                Content = "A deep dive into the benefits and challenges of microservices.",
                Category = "Architecture",
                Tags = new List<string> { "microservices", "architecture", "distributed" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Getting started with Kubernetes",
                Content = "A beginner's guide to deploying and managing containerized applications with Kubernetes.",
                Category = "DevOps",
                Tags = new List<string> { "kubernetes", "containers", "devops" },
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
                Tags = new List<string> { "docker", "containers", "devops" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Implementing CI/CD pipelines",
                Content = "A practical guide to setting up continuous integration and continuous deployment pipelines.",
                Category = "DevOps",
                Tags = new List<string> { "ci", "cd", "devops" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Unit testing strategies in .NET",
                Content = "Effective approaches for writing maintainable unit tests using xUnit and mocking frameworks.",
                Category = "Testing",
                Tags = new List<string> { "testing", "xunit", "dotnet" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Building interactive UIs with Blazor",
                Content = "An introduction to Blazor for creating client-side web UIs with C# instead of JavaScript.",
                Category = "Frontend",
                Tags = new List<string> { "blazor", "web", "frontend" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "EF Core migrations demystified",
                Content = "How to manage database schema changes safely using Entity Framework Core migrations.",
                Category = "Database",
                Tags = new List<string> { "efcore", "database", "migrations" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Security best practices for web apps",
                Content = "Essential security measures to protect web applications from common threats.",
                Category = "Security",
                Tags = new List<string> { "security", "web", "best-practices" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Performance tuning .NET applications",
                Content = "Tips and tools for diagnosing and improving performance in .NET applications.",
                Category = "Performance",
                Tags = new List<string> { "performance", "profiling", "dotnet" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Principles of clean code",
                Content = "Guidelines and practices to write readable, maintainable, and testable code.",
                Category = "Software Engineering",
                Tags = new List<string> { "clean-code", "best-practices", "software-engineering" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Designing cloud-native applications on Azure",
                Content = "Architectural patterns and services to build scalable cloud-native solutions on Azure.",
                Category = "Cloud",
                Tags = new List<string> { "azure", "cloud", "architecture" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Observability: logging, metrics, and tracing",
                Content = "How to gain actionable insights into system behavior using observability practices.",
                Category = "Operations",
                Tags = new List<string> { "observability", "logging", "tracing" },
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
                Tags = new List<string> { "docker", "containers", "devops" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Implementing CI/CD pipelines",
                Content = "A practical guide to setting up continuous integration and continuous deployment pipelines.",
                Category = "DevOps",
                Tags = new List<string> { "ci", "cd", "devops" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Unit testing strategies in .NET",
                Content = "Effective approaches for writing maintainable unit tests using xUnit and mocking frameworks.",
                Category = "Testing",
                Tags = new List<string> { "unit-testing", "xunit", "mocking" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Building interactive UIs with Blazor",
                Content = "An introduction to Blazor for creating client-side web UIs with C# instead of JavaScript.",
                Category = "Frontend",
                Tags = new List<string> { "blazor", "frontend", "csharp" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "EF Core migrations demystified",
                Content = "How to manage database schema changes safely using Entity Framework Core migrations.",
                Category = "Database",
                Tags = new List<string> { "ef-core", "migrations", "database" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Security best practices for web apps",
                Content = "Essential security measures to protect web applications from common threats.",
                Category = "Security",
                Tags = new List<string> { "security", "web", "best-practices" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Performance tuning .NET applications",
                Content = "Tips and tools for diagnosing and improving performance in .NET applications.",
                Category = "Performance",
                Tags = new List<string> { "performance", "dotnet", "optimization" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Principles of clean code",
                Content = "Guidelines and practices to write readable, maintainable, and testable code.",
                Category = "Software Engineering",
                Tags = new List<string> { "clean-code", "software-engineering", "best-practices" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Designing cloud-native applications on Azure",
                Content = "Architectural patterns and services to build scalable cloud-native solutions on Azure.",
                Category = "Cloud",
                Tags = new List<string> { "cloud", "azure", "architecture" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Observability: logging, metrics, and tracing",
                Content = "How to gain actionable insights into system behavior using observability practices.",
                Category = "Operations",
                Tags = new List<string> { "observability", "logging", "metrics", "tracing" },
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        context.Posts.AddRange(defaultPosts);
        context.SaveChanges();
    }
}
