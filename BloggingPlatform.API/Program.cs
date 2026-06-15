using BloggingPlatform.API.Data;
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

var app = builder.Build();

app.MapControllers();

app.Run();
