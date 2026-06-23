using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.API.DTOs;

// GET /posts?page=2&pageSize=10&term=tech
public record GetPostsQuery
{
    private const int MaximumPageSize = 50; // Hard limit to protect the database
    private int _page = 1;
    private int _pageSize = 10;

    [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
    public int Page
    {
        get => _page;
        init => _page = value < 1 ? 1 : value; // Fallback to page 1 if invalid
    }

    [Range(1, MaximumPageSize, ErrorMessage = "Page size must be between 1 and 50")]
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value > MaximumPageSize ? MaximumPageSize : (value < 1 ? 10 : value);
    }

    [StringLength(100, ErrorMessage = "Search term cannot be longer than 100 characters")]
    public string? Term { get; init; }
}

