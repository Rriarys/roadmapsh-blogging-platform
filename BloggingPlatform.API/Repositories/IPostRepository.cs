using BloggingPlatform.API.Models;

namespace BloggingPlatform.API.Repositories;

public interface IPostRepository
{
    Task<Post?> GetByIdAsync(Guid postId);
    Task<Post> CreateAsync(Post postEntity);
    Task UpdateAsync(Post postEntity);
    Task DeleteAsync(Post postEntity);

    /// <summary>
    /// Retrieves a paginated list of posts with optional search filtering
    /// </summary>
    /// <param name="pageNumber">The 1-based page number to retrieve</param>
    /// <param name="pageSize">The number of posts per page</param>
    /// <param name="searchTerm">Optional search term to filter posts by title, content, or category (case-insensitive). Pass null or whitespace
    /// to retrieve all posts</param>
    /// <returns>A tuple containing the posts for the specified page and the total count of all matching posts</returns>
    Task<(IReadOnlyCollection<Post> Items, int TotalCount)> GetPagedListAsync(int pageNumber, int pageSize, string? searchTerm);
}
