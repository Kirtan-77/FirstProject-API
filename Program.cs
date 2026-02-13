using Microsoft.EntityFrameworkCore;
using FirstProject.Api.Data;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWindowsService();
builder.WebHost.UseUrls("http://localhost:5000");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<Appdbcontext>(options =>
    options.UseSqlite("Data Source=products.db"));
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Appdbcontext>();
    db.Database.EnsureDeleted();   // deletes broken DB
    db.Database.EnsureCreated();   // recreates tables directly
}
app.Run();
