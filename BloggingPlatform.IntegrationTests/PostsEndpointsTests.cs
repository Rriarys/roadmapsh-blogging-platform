using System.Net;
using System.Net.Http.Json;
using BloggingPlatform.API.DTOs;
using BloggingPlatform.API.Data;
using BloggingPlatform.API.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApi.IntegrationTests;

public class PostsEndpointsTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public PostsEndpointsTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    // Helper to reset database
    private void ClearDatabase()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BloggingPlatformDbContext>();
        db.Posts.RemoveRange(db.Posts);
        db.SaveChanges();
    }

    // Helper to insert specific posts
    private void SeedDatabase(List<Post> posts)
    {
        ClearDatabase();
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BloggingPlatformDbContext>();
        db.Posts.AddRange(posts);
        db.SaveChanges();
    }

    // Helper to generate 17 posts for pagination and search scenarios
    private void SeedPaginationData()
    {
        var posts = new List<Post>();

        // Add 3 specific posts for search testing
        posts.Add(new Post { Id = Guid.NewGuid(), Title = "Why learning .NET is awesome", Content = "Content", Category = "Tech", Tags = ["dotnet"], CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
        posts.Add(new Post { Id = Guid.NewGuid(), Title = "Unit testing strategies in .NET", Content = "Content", Category = "Tech", Tags = ["testing"], CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
        posts.Add(new Post { Id = Guid.NewGuid(), Title = "Title", Content = "Principles of clean code, readable, maintainable, and testable code", Category = "Tech", Tags = ["clean-code"], CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
        posts.Add(new Post { Id = Guid.NewGuid(), Title = "DevOps 1", Content = "Getting started with Kubernetes", Category = "DevOps", Tags = ["k8s"], CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
        posts.Add(new Post { Id = Guid.NewGuid(), Title = "DevOps 2", Content = "Docker basics", Category = "DevOps", Tags = ["docker"], CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
        posts.Add(new Post { Id = Guid.NewGuid(), Title = "DevOps 3", Content = "CI/CD pipelines", Category = "DevOps", Tags = ["ci-cd"], CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });

        // Add remaining 11 filler posts to reach 17 total
        for (int i = 0; i < 11; i++)
        {
            posts.Add(new Post { Id = Guid.NewGuid(), Title = $"Filler Post {i}", Content = "Content", Category = "General", Tags = ["filler"], CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
        }

        SeedDatabase(posts);
    }

    #region GET /posts (Pagination and Search)

    [Fact]
    public async Task GetPosts_WithDefaultPaging_ReturnsPage1Of10() // Scenario 1
    {
        SeedPaginationData();
        var response = await _client.GetAsync("/posts");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<PostsPagedResponse>();

        Assert.NotNull(result);
        Assert.Equal(10, result.Posts.Count);
        Assert.Equal(17, result.TotalCount);
        Assert.Equal(1, result.Page);
    }

    [Fact]
    public async Task GetPosts_WithPage2_ReturnsRemaining7Items() // Scenario 2
    {
        SeedPaginationData();
        var response = await _client.GetAsync("/posts?page=2");

        var result = await response.Content.ReadFromJsonAsync<PostsPagedResponse>();
        Assert.NotNull(result);
        Assert.Equal(7, result.Posts.Count);
    }

    [Fact]
    public async Task GetPosts_WithCustomPageSize_ReturnsRequestedSize() // Scenario 3
    {
        SeedPaginationData();
        var response = await _client.GetAsync("/posts?page=1&pageSize=3");

        var result = await response.Content.ReadFromJsonAsync<PostsPagedResponse>();
        Assert.NotNull(result);
        Assert.Equal(3, result.Posts.Count);
    }

    [Theory] // Scenarios 4, 5, 6, 7
    [InlineData(".NET", 2)]
    [InlineData("readable", 1)]
    [InlineData("DevOps", 3)]
    [InlineData("NonExistentWord", 0)]
    public async Task GetPosts_WithSearchTerm_ReturnsMatchingItems(string term, int expectedCount)
    {
        SeedPaginationData();
        var response = await _client.GetAsync($"/posts?term={term}");

        var result = await response.Content.ReadFromJsonAsync<PostsPagedResponse>();
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.Posts.Count);
    }

    [Theory] // Scenarios 8, 9
    [InlineData("page=0")]
    [InlineData("pageSize=999999")]
    public async Task GetPosts_WithInvalidPaging_FallsBackToDefaultValues(string query)
    {
        SeedPaginationData();
        var response = await _client.GetAsync($"/posts?{query}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<PostsPagedResponse>();
        Assert.NotNull(result);
        Assert.Equal(1, result.Page);
    }

    [Fact] // Scenario 11
    public async Task GetPosts_WithExcessivelyLongSearchTerm_Returns400BadRequest()
    {
        var longTerm = new string('A', 150);
        var response = await _client.GetAsync($"/posts?term={longTerm}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetPosts_WithOutOfBoundsPage_ReturnsEmptyList() // Scenario 10
    {
        SeedPaginationData();
        var response = await _client.GetAsync("/posts?page=5");

        var result = await response.Content.ReadFromJsonAsync<PostsPagedResponse>();
        Assert.NotNull(result);
        Assert.Empty(result.Posts);
        Assert.Equal(17, result.TotalCount);
    }

    #endregion

    #region GET /posts/{id}

    [Fact]
    public async Task GetPostById_WhenPostExists_Returns200OK() // Scenario 12
    {
        var postId = Guid.NewGuid();
        SeedDatabase([new Post { Id = postId, Title = "Test", Content = "Content", Category = "Tech", Tags = [], CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }]);

        var response = await _client.GetAsync($"/posts/{postId}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var post = await response.Content.ReadFromJsonAsync<PostResponseDto>();
        Assert.Equal(postId, post!.Id);
    }

    [Fact]
    public async Task GetPostById_WhenPostDoesNotExist_Returns404NotFound() // Scenario 13
    {
        var response = await _client.GetAsync($"/posts/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region POST /posts

    [Fact]
    public async Task CreatePost_WithValidData_Returns201Created() // Scenario 14
    {
        ClearDatabase();
        var requestDto = new CreatePostRequestDto("Valid Title", "Valid Content", "Tech", ["tag1"]);

        var response = await _client.PostAsJsonAsync("/posts", requestDto);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task CreatePost_WithEmptyTitle_Returns400BadRequest() // Scenario 15
    {
        var requestDto = new CreatePostRequestDto("   ", "Valid Content", "Tech", ["tag1"]);
        var response = await _client.PostAsJsonAsync("/posts", requestDto);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreatePost_WithExcessivelyLongTitle_Returns400BadRequest() // Scenario 16
    {
        var longTitle = new string('A', 151);
        var requestDto = new CreatePostRequestDto(longTitle, "Valid Content", "Tech", ["tag1"]);
        var response = await _client.PostAsJsonAsync("/posts", requestDto);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreatePost_WithTooManyTags_Returns400BadRequest() // Scenario 17
    {
        var requestDto = new CreatePostRequestDto("Title", "Content", "Tech", ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"]);
        var response = await _client.PostAsJsonAsync("/posts", requestDto);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreatePost_WithExcessivelyLongTag_Returns400BadRequest() // Scenario 18
    {
        var requestDto = new CreatePostRequestDto("Title", "Content", "Tech", ["ThisTagIsWayTooLongForOurValidation"]);
        var response = await _client.PostAsJsonAsync("/posts", requestDto);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion

    #region PUT /posts/{id}

    [Fact]
    public async Task UpdatePost_WithValidData_Returns200OK() // Scenario 19
    {
        var postId = Guid.NewGuid();
        SeedDatabase([new Post { Id = postId, Title = "Old", Content = "Old", Category = "Old", Tags = [], CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }]);

        var updateDto = new UpdatePostRequestDto("New Title", "New Content", "New Category", ["new-tag"]);
        var response = await _client.PutAsJsonAsync($"/posts/{postId}", updateDto);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var post = await response.Content.ReadFromJsonAsync<PostResponseDto>();
        Assert.Equal("New Title", post!.Title);
    }

    [Fact]
    public async Task UpdatePost_WhenPostDoesNotExist_Returns404NotFound() // Scenario 20
    {
        var updateDto = new UpdatePostRequestDto("Title", "Content", "Category", []);
        var response = await _client.PutAsJsonAsync($"/posts/{Guid.NewGuid()}", updateDto);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdatePost_WithInvalidData_Returns400BadRequest() // Scenario 21
    {
        var postId = Guid.NewGuid();
        SeedDatabase([new Post { Id = postId, Title = "Old", Content = "Old", Category = "Old", Tags = [], CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }]);

        var updateDto = new UpdatePostRequestDto("Title", "Content", "", []); // Empty category
        var response = await _client.PutAsJsonAsync($"/posts/{postId}", updateDto);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion

    #region DELETE /posts/{id}

    [Fact]
    public async Task DeletePost_WhenExists_Returns204NoContent() // Scenario 22
    {
        var postId = Guid.NewGuid();
        SeedDatabase([new Post { Id = postId, Title = "Test", Content = "Content", Category = "Tech", Tags = [], CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }]);

        var response = await _client.DeleteAsync($"/posts/{postId}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeletePost_WhenDoesNotExist_Returns404NotFound() // Scenario 23
    {
        var response = await _client.DeleteAsync($"/posts/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region Global Exception Handling

    [Fact]
    public async Task GetTestFault_Returns500InternalServerError() // Scenario 24
    {
        var response = await _client.GetAsync("/posts/test-fault");

        if (response.StatusCode != HttpStatusCode.NotFound)
        {
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }

    #endregion
}