using FirstProject.Api.Data;
using FirstProject.Api.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWindowsService();
builder.WebHost.UseUrls("http://localhost:5000");

// Add services to the container.

builder.Services.AddDbContext<Appdbcontext>(options =>
    options.UseSqlite("Data Source=products.db"));
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.SetIsOriginAllowed(_ => true)  // Allow any origin
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});



var app = builder.Build();

// Database initialization
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Appdbcontext>();
    db.Database.EnsureCreated();
    //Console.WriteLine("✅ Database initialized");
}

// CRITICAL: CORS must come FIRST, before anything else!
app.UseCors("AllowFrontend");
Console.WriteLine("✅ CORS enabled for: http://localhost:5000");

// Development tools
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Routing must come after CORS
app.UseRouting();
    
// Authentication/Authorization
app.UseAuthorization();
app.MapHub<FirstProject.Api.Hubs.MRPUpdateHub>("/mrpHub");

app.Run();