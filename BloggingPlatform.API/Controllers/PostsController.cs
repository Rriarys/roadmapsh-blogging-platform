using BloggingPlatform.API.DTOs;
using BloggingPlatform.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.API.Controllers;

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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PostResponseDto>> GetPostById(Guid id)
    {
        var post = await postService.GetPostByIdAsync(id);

        if (post is null) return NotFound(); // 404 Not Found if the post doesn't exist

        // 200 OK with the post data
        return Ok(post); 
    }

    [HttpPost]
    public async Task<ActionResult<PostResponseDto>> CreatePost([FromBody] CreatePostRequestDto createDto)
    {
        var createdPost = await postService.CreatePostAsync(createDto);

        // Returns 201 Created with the location of the new resource
        return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
    }
}
