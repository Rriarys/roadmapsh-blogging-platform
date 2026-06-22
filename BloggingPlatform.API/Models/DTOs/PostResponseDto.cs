namespace BloggingPlatform.API.Models.DTOs;

public record PostResponseDto(
    string Title,
    string Content,
    string Category
);
