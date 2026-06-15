using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.API.Data;

public class BloggingPlatformDbContext : DbContext
{
    public BloggingPlatformDbContext(DbContextOptions<BloggingPlatformDbContext> options) : base(options)
    {
    }
}
