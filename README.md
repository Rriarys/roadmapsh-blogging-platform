# Blogging Platform API

Small educational pet project from the roadmap.sh project ideas pool:

https://roadmap.sh/projects/blogging-platform-api

The repository contains the API project and a separate integration test project.

## Overview

The API manages blog posts and exposes endpoints for creating, reading, updating, deleting, listing, and searching posts.

The application stores data in SQLite, applies EF Core migrations automatically on startup, and seeds the database with sample posts when the database is empty.

## Responsibilities

### API

- Accept blog post CRUD requests over HTTP
- Store posts in SQLite through Entity Framework Core
- Return paginated post lists
- Search posts by term in `title`, `content`, and `category`
- Validate incoming request payloads and query parameters
- Apply database migrations and seed sample data on startup
- Handle expected and unexpected failures through centralized exception handling

### Integration tests

- Verify list pagination behavior
- Verify search behavior
- Verify single-item read behavior
- Verify create, update, and delete flows
- Verify request validation failures
- Verify global error handling behavior

## How It Works

1. The client sends a request to one of the `/posts` endpoints
2. The controller delegates the operation to the application service
3. The service validates and prepares data for persistence
4. The repository reads from or writes to SQLite through EF Core
5. The API returns JSON responses for item and list operations
6. On normal application startup, migrations are applied automatically and sample posts are inserted if the database is empty

## Features

- .NET 10 backend
- ASP.NET Core Web API with controllers
- SQLite database storage
- Entity Framework Core migrations
- Automatic database initialization on startup
- CRUD endpoints for blog posts
- Paginated list endpoint
- Search by title, content, and category
- Request validation for body and query parameters
- Centralized exception handling
- Integration test coverage for the implemented API behavior

## Tech Stack

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- SQLite
- xUnit
- Microsoft.AspNetCore.Mvc.Testing

## Data Model

Each post is stored with:

- `id`
- `title`
- `content`
- `category`
- `tags`
- `createdAt`
- `updatedAt`

The public API responses currently return:

- `id`
- `title`
- `content`
- `category`
- `tags`

## API Endpoints

### List posts

**Method:** `GET`  
**Route:** `/posts`

#### Query parameters

- `page` - optional, default `1`
- `pageSize` - optional, default `10`, maximum `50`
- `term` - optional search term, matched against `title`, `content`, and `category`

#### Successful response shape

```json
{
  "posts": [
	{
	  "id": "7e70c2b5-3f8f-4d43-9b2f-3ed540a70f13",
	  "title": "Why learning .NET is awesome",
	  "content": "C# and .NET provide incredible performance and great developer experience.",
	  "category": "Education",
	  "tags": ["dotnet", "csharp", "learning"]
	}
  ],
  "totalCount": 1,
  "page": 1,
  "pageSize": 10
}
```

### Get post by id

**Method:** `GET`  
**Route:** `/posts/{id}`

### Create post

**Method:** `POST`  
**Route:** `/posts`

### Update post

**Method:** `PUT`  
**Route:** `/posts/{id}`

### Delete post

**Method:** `DELETE`  
**Route:** `/posts/{id}`

## Validation Notes

- `title` is required and limited to 150 characters
- `content` is required and limited to 10000 characters
- `category` is required and limited to 50 characters
- `tags` accepts up to 10 items
- each `tag` is limited to 20 characters
- `term` is limited to 100 characters

The service also trims tags, removes empty values, and stores distinct tag values only.

## Run the Project

### Prerequisites

- .NET 10 SDK

### Start the API

```sh
dotnet run --project BloggingPlatform.API
```

The default local URL is `http://localhost:5104`.

## Database Migrations and Initialization

The API is configured to apply EF Core migrations automatically on startup and seed sample posts when the database is empty.

In the default setup, simply starting the API is enough to create or update the SQLite database.

Starting the API will:

- create the SQLite database if it does not exist
- apply the existing migrations
- insert sample posts if the `Posts` table is empty

### Manual migration update

If you want to apply migrations explicitly:

```sh
dotnet ef database update --project BloggingPlatform.API --startup-project BloggingPlatform.API
```

## Example Requests

The repository already includes a request collection here:

- `BloggingPlatform.API/BloggingPlatform.API.http`

In Visual Studio 2026, this HTTP file can send requests directly from the IDE while the API is running and show the responses in the editor.

You can also send the same requests with curl.

Base URL used below:

```text
http://localhost:5104
```

### Create a post

```sh
curl -X POST http://localhost:5104/posts \
  -H "Content-Type: application/json" \
  -d '{
	"title": "Building APIs with ASP.NET Core",
	"content": "This post walks through a simple controller-based API.",
	"category": "Backend",
	"tags": ["dotnet", "aspnet", "api"]
  }'
```

### Get a paginated list of posts

```sh
curl "http://localhost:5104/posts?page=1&pageSize=3"
```

### Search posts

```sh
curl "http://localhost:5104/posts?term=DevOps"
```

### Get a post by id

```sh
curl http://localhost:5104/posts/7e70c2b5-3f8f-4d43-9b2f-3ed540a70f13
```

### Update a post

```sh
curl -X PUT http://localhost:5104/posts/7e70c2b5-3f8f-4d43-9b2f-3ed540a70f13 \
  -H "Content-Type: application/json" \
  -d '{
	"title": "Building APIs with ASP.NET Core and EF Core",
	"content": "This post was updated with more implementation details.",
	"category": "Backend",
	"tags": ["dotnet", "efcore", "api"]
  }'
```

### Delete a post

```sh
curl -X DELETE http://localhost:5104/posts/7e70c2b5-3f8f-4d43-9b2f-3ed540a70f13
```

## Testing

Run the tests from the repository root:

```sh
dotnet test
```

The test project covers:

- create, read, update, and delete scenarios
- paginated list behavior
- search behavior
- validation failures
- not found cases
- global exception handling behavior
