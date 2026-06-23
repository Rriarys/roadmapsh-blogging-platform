# Blogging Platform API

A small educational project based on [roadmap.sh/projects/blogging-platform-api](https://roadmap.sh/projects/blogging-platform-api).

The API manages blog posts with full CRUD support, pagination, searching, and request validation.

## Tech Stack
- .NET 10 & ASP.NET Core
- Entity Framework Core 10 (SQLite)
- xUnit & Microsoft.AspNetCore.Mvc.Testing

## API Endpoints
All requests use application/json.

| Method | Route | Description |
| :--- | :--- | :--- |
| GET | /posts | List posts (with pagination & search) |
| GET | /posts/{id} | Get post by ID |
| POST | /posts | Create new post |
| PUT | /posts/{id} | Update existing post |
| DELETE | /posts/{id} | Delete post |

### Paginated Response Example
```
{
  "posts": [
    {
      "id": "7e70c2b5-3f8f-4d43-9b2f-3ed540a70f13",
      "title": "Example Post",
      "content": "Example content",
      "category": "Tech",
      "tags": ["dotnet"],
      "createdAt": "2026-06-23T10:00:00Z",
      "updatedAt": "2026-06-23T10:05:00Z"
    }
  ],
  "totalCount": 17,
  "page": 1,
  "pageSize": 10
}
```

## Validation Rules
- `Title`: Required, max 150 chars.
- `Content`: Required, max 10000 chars.
- `Category`: Required, max 50 chars.
- `Tags`: Max 10 items, each max 20 chars (auto-trimmed & distinct).
- `Search Term`: Max 100 chars.

## Getting Started
1. Run the API:
```
   dotnet run --project BloggingPlatform.API
```
   The database (SQLite) and migrations are applied automatically on startup.

2. Run Tests:
```
   dotnet test
```
   The test suite covers full lifecycle scenarios, validation, and error handling.

## Manual Testing
- Request Testing: Use `BloggingPlatform.API/BloggingPlatform.API.http` within your IDE to execute requests against the running API.