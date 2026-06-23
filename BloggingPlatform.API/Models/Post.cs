namespace BloggingPlatform.API.Models;

// Include fields for Id, Title, Content, Category, CreatedAt, and UpdatedAt
public class Post
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Category { get; set; } = null!;
    public List<string> Tags { get; set; } = new List<string>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
