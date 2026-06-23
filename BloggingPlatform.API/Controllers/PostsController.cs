using BloggingPlatform.API.DTOs;
using BloggingPlatform.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace BloggingPlatform.API.Controllers;

//Add GET /posts to return a paged list of blog posts
//Support query parameters page, pageSize, and optional term
//Apply wildcard search to Title, Content, and Category
//Return pagination metadata together with the current page of items
//Keep ordering deterministic for stable paging
[ApiController]
[Route("posts")]
public class PostsController(IPostService postService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PostsPagedResponse>> GetPosts([FromQuery] GetPostsQuery query)
    {
        // Coordinates controller request and repository access via service
        var pagedResponse = await postService.GetPagedPostsAsync(
            query.Page,
            query.PageSize,
            query.Term
        );

        // Returns 200 OK with pagination metadata and items
        return Ok(pagedResponse);
    }
}
