namespace BloggingPlatform.API.DTOs;

public record CreatePostRequestDto(
    string Title,
    string Content,
    string Category
);
