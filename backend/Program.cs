using Microsoft.EntityFrameworkCore;
using LearnerCenter.API.Data;
using LearnerCenter.API.Interfaces;
using LearnerCenter.API.Repositories;
using LearnerCenter.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework
builder.Services.AddDbContext<LearnerCenterDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (!string.IsNullOrEmpty(connectionString) && connectionString.Contains("Host="))
    {
        // PostgreSQL connection (production)
        options.UseNpgsql(connectionString);
    }
    else
    {
        // SQL Server connection (development)
        options.UseSqlServer(connectionString ?? "");
    }
});

// Add Controllers
builder.Services.AddControllers();

// Add CORS for frontend development  
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
        else
        {
            // Allow production hosting domains
            policy.SetIsOriginAllowed(origin => 
                {
                    var uri = new Uri(origin);
                    return uri.Host.EndsWith(".vercel.app") ||
                           uri.Host.EndsWith(".railway.app") ||
                           uri.Host == "localhost";
                })
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        }
    });
});

// Register repository and service dependencies
builder.Services.AddScoped<ICampusRepository, CampusRepository>();
builder.Services.AddScoped<ICampusService, CampusService>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Add API documentation services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "LearnerCenter API",
        Version = "v1",
        Description = "API for managing learner center operations including campuses, enrollments, and courses"
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Auto-migrate database on startup (non-blocking)
_ = Task.Run(async () =>
{
    try
    {
        await Task.Delay(5000); // Wait 5 seconds for app to start
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LearnerCenterDbContext>();
        await context.Database.MigrateAsync();
        Console.WriteLine("Database migration completed successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration failed (non-blocking): {ex.Message}");
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LearnerCenter API v1");
        c.RoutePrefix = "swagger"; // This makes swagger available at /swagger
    });
    app.MapOpenApi();
}

// Use CORS before other middleware
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

// Add health check endpoint
app.MapGet("/", () => "LearnerCenter API is running!");
app.MapGet("/api/health", () => new { status = "healthy", timestamp = DateTime.UtcNow });

// Map controllers
app.MapControllers();

app.Run();
