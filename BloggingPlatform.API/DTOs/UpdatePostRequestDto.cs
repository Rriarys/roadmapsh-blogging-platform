namespace BloggingPlatform.API.DTOs;

public record UpdatePostRequestDto(
    string Title,
    string Content,
    string Category
);
