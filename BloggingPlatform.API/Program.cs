using BloggingPlatform.API.Middlewares;
using BloggingPlatform.API.Data;
using BloggingPlatform.API.Data.DataExtensions;
using BloggingPlatform.API.Repositories;
using BloggingPlatform.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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

app.UseExceptionHandler();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.Services.ApplyMigrations();
}

app.MapControllers();

app.Run();
