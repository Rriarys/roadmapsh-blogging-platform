namespace BloggingPlatform.API.DTOs;

public record PostsPagedResponse(
    IReadOnlyCollection<PostResponseDto> Posts,
    int TotalCount,
    int Page,
    int PageSize
);
