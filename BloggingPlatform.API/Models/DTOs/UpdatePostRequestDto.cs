namespace BloggingPlatform.API.Models.DTOs;

public record UpdatePostRequestDto(
    string Title,
    string Content,
    string Category
);
