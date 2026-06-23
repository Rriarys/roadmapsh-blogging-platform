using BloggingPlatform.API.DTOs;
using BloggingPlatform.API.Repositories;

namespace BloggingPlatform.API.Services;

public class PostService(IPostRepository postRepository) : IPostService
{
    public async Task<PostResponseDto?> GetPostByIdAsync(Guid postId)
    {
        var postEntity = await postRepository.GetByIdNoTrackingAsync(postId); // using GetByIdNoTrackingAsync for read-only operation
        if (postEntity is null) return null;

        return new PostResponseDto(postEntity.Id, postEntity.Title, postEntity.Content, postEntity.Category);
    }

    public async Task<PostResponseDto> CreatePostAsync(CreatePostRequestDto createDto)
    {
        var currentDateTime = DateTime.UtcNow;

        var postEntity = new Models.Post
        {
            Title = createDto.Title,
            Content = createDto.Content,
            Category = createDto.Category,
            CreatedAt = currentDateTime,
            UpdatedAt = currentDateTime
        };
        var createdPost = await postRepository.CreateAsync(postEntity);

        return new PostResponseDto(createdPost.Id, createdPost.Title, createdPost.Content, createdPost.Category);
    }

    public async Task<PostResponseDto?> UpdatePostAsync(Guid postId, UpdatePostRequestDto updateDto)
    {
        var existingEntity = await postRepository.GetByIdAsync(postId);
        if (existingEntity is null) return null; // null for 404 Not Found

        existingEntity.Title = updateDto.Title;
        existingEntity.Content = updateDto.Content;
        existingEntity.Category = updateDto.Category;
        existingEntity.UpdatedAt = DateTime.UtcNow;

        await postRepository.UpdateAsync(existingEntity);

        return new PostResponseDto(existingEntity.Id, existingEntity.Title, existingEntity.Content, existingEntity.Category);
    }

    public async Task<bool> DeletePostAsync(Guid postId)
    {
        var existingEntity = await postRepository.GetByIdAsync(postId);
        if (existingEntity is null) return false;

        await postRepository.DeleteAsync(existingEntity);

        return true; // true for successful deletion
    }

    public async Task<PostsPagedResponse> GetPagedPostsAsync(int pageNumber, int pageSize, string? searchTerm)
    {
        var (posts, totalCount) = await postRepository.GetPagedListAsync(pageNumber, pageSize, searchTerm);

        var postDtosCollection = posts.Select(post =>
            new PostResponseDto(post.Id, post.Title, post.Content, post.Category))
            .ToList(); // packing post entities into PostResponseDto collection

        return new PostsPagedResponse(postDtosCollection, totalCount, pageNumber, pageSize);
    }
}
