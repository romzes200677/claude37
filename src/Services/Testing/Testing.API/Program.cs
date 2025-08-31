using Microsoft.EntityFrameworkCore;
using Testing.Application;
using Testing.Infrastructure;
using Testing.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Add database context
builder.Services.AddDbContext<TestingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestingDatabase")));

// Add application services
builder.Services.AddApplicationServices();

// Add infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    // Initialize and seed the database in development environment
    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<TestingDbInitializer>();
        await initializer.InitializeAsync();
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
