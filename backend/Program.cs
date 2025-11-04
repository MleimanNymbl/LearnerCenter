using Microsoft.EntityFrameworkCore;
using LearnerCenter.API.Data;
using LearnerCenter.API.Interfaces;
using LearnerCenter.API.Repositories;
using LearnerCenter.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework with dynamic database provider selection
builder.Services.AddDbContext<LearnerCenterDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    try 
    {
        Console.WriteLine($"Connection string: {connectionString}");
        Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
        
        // Use PostgreSQL in production, SQL Server in development/local
        if (builder.Environment.IsProduction())
        {
            Console.WriteLine("Using PostgreSQL");
            options.UseNpgsql(connectionString);
        }
        else
        {
            Console.WriteLine("Using SQL Server");
            options.UseSqlServer(connectionString);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database config error: {ex}");
        throw;
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
            // Allow production hosting domains including Azure Static Web Apps
            policy.WithOrigins(
                "https://lemon-smoke-05044a110.3.azurestaticapps.net",
                "https://lemon-smoke-05044a110-preview.centralus.3.azurestaticapps.net"
            )
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

// Auto-migrate database on startup
try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<LearnerCenterDbContext>();
    
    Console.WriteLine("Starting database migration...");
    await context.Database.MigrateAsync();
    Console.WriteLine("Database migration completed successfully");
}
catch (Exception ex)
{
    Console.WriteLine($"Migration failed: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
    // Don't fail startup, just log the error
}

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

// Add database connection test endpoint
app.MapGet("/api/test-db", async (LearnerCenterDbContext context) =>
{
    try
    {
        var canConnect = await context.Database.CanConnectAsync();
        return Results.Ok(new { canConnect, message = canConnect ? "Database connection successful" : "Database connection failed" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Database error: {ex.Message}");
    }
});

// Add manual migration endpoint
app.MapPost("/api/migrate", async (LearnerCenterDbContext context) =>
{
    try
    {
        await context.Database.MigrateAsync();
        return Results.Ok(new { message = "Migration completed successfully", timestamp = DateTime.UtcNow });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Migration failed: {ex.Message}");
    }
});

// Map controllers
app.MapControllers();

app.Run();
