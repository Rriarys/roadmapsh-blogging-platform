using BloggingPlatform.API.DTOs;

namespace BloggingPlatform.API.Services;

public interface IPostService
{
    Task<PostResponseDto?> GetPostByIdAsync(Guid postId);
    Task<PostResponseDto> CreatePostAsync(CreatePostRequestDto createDto);
    Task<PostResponseDto?> UpdatePostAsync(Guid postId, UpdatePostRequestDto updateDto);
    Task<bool> DeletePostAsync(Guid postId);
    Task<PostsPagedResponse> GetPagedPostsAsync(int pageNumber, int pageSize, string? searchTerm);
}
