namespace BloggingPlatform.API.Models.DTOs;

public record CreatePostRequestDto(
    string Title,
    string Content,
    string Category
);
