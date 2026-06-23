using BloggingPlatform.API.DTOs.DtoHelpers;
using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.API.DTOs;

public record CreatePostRequestDto(
    [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
    [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters.")]
    string Title,

    [Required(AllowEmptyStrings = false, ErrorMessage = "Content is required.")]
    [StringLength(10000, ErrorMessage = "Content cannot exceed 10000 characters.")]
    string Content,

    [Required(AllowEmptyStrings = false, ErrorMessage = "Category is required.")]
    [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters.")]
    string Category,

    [MaxLength(10, ErrorMessage = "You can add a maximum of 10 tags")]
    [TagLengthValidator(20, ErrorMessage = "Each tag must be 20 characters or less")]
    List<string> Tags
);
