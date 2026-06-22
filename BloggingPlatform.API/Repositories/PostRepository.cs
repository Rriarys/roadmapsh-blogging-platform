using BloggingPlatform.API.Data;
using BloggingPlatform.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.API.Repositories;

public class PostRepository(BloggingPlatformDbContext dbContext) : IPostRepository
{
    public async Task<Post?> GetByIdAsync(Guid postId)
    {
        return await dbContext.Posts.FindAsync(postId);
    }

    public async Task<Post> CreateAsync(Post postEntity)
    {
        dbContext.Posts.Add(postEntity);
        await dbContext.SaveChangesAsync();
        return postEntity;
    }

    public async Task UpdateAsync(Post postEntity)
    {
        dbContext.Posts.Update(postEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post postEntity)
    {
        dbContext.Posts.Remove(postEntity);
        await dbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<(IReadOnlyCollection<Post> Items, int TotalCount)> GetPagedListAsync(int pageNumber, int pageSize, string? searchTerm)
    {
        // Prepare future SQL query with AsNoTracking for better performance since we are only reading data
        var queryablePosts = dbContext.Posts.AsNoTracking();

        // If a search term is provided, filter the posts by title, content, or category
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var lowerSearchTerm = searchTerm.ToLower();
            queryablePosts = queryablePosts.Where(post =>
                post.Title.ToLower().Contains(lowerSearchTerm) ||
                post.Content.ToLower().Contains(lowerSearchTerm) ||
                post.Category.ToLower().Contains(lowerSearchTerm)); // ToLower() - issue for SQLite Db, for other Db not needed
        }

        // Create SQL request to count total items matching the search term
        var totalPostsCount = await queryablePosts.CountAsync();

        // Creating SQL request with pagination parameters
        var postsCollection = await queryablePosts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (postsCollection, totalPostsCount);
    }
}
