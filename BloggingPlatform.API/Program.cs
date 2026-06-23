using BloggingPlatform.API.Data;
using BloggingPlatform.API.Data.DataExtensions;
using BloggingPlatform.API.Repositories;
using BloggingPlatform.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

string? SQLiteConnectionString = builder.Configuration.GetConnectionString("SQLiteConnection");
if (string.IsNullOrWhiteSpace(SQLiteConnectionString))
{
    throw new InvalidOperationException("Connection string 'SQLiteConnection' not found.");
}
builder.Services.AddDbContext<BloggingPlatformDbContext>(options =>
    options.UseSqlite(SQLiteConnectionString));

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

app.Services.ApplyMigrations();

app.MapControllers();

app.Run();
