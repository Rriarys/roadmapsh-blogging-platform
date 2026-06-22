namespace BloggingPlatform.API.Models.DTOs;

// GET /posts?page=2&pageSize=10&term=tech
public record GetPostsQuery(
    int Page = 1,
    int PageSize = 10,
    string? Term = null
);

