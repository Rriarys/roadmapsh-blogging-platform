using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.API.DTOs;

public record CreatePostRequestDto(
    [property: Required(AllowEmptyStrings = false, ErrorMessage = "Title is required and cannot be empty.")]
    string Title,
    [property: Required(AllowEmptyStrings = false, ErrorMessage = "Content is required and cannot be empty.")]
    string Content,
    [property: Required(AllowEmptyStrings = false, ErrorMessage = "Category is required and cannot be empty.")]
    string Category
);
