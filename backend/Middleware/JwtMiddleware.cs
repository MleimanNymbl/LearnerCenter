using System.Security.Claims;
using LearnerCenter.API.Services;

namespace LearnerCenter.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IJwtService jwtService)
        {
            var token = ExtractTokenFromHeader(context);

            // Only validate token if present - don't block requests without tokens
            if (!string.IsNullOrEmpty(token))
            {
                await AttachUserToContext(context, jwtService, token);
            }
            // If no token, just continue - let [Authorize] attributes handle protection

            await _next(context);
        }

        private string? ExtractTokenFromHeader(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            
            if (authHeader != null && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return authHeader.Substring("Bearer ".Length).Trim();
            }

            return null;
        }

        private Task AttachUserToContext(HttpContext context, IJwtService jwtService, string token)
        {
            try
            {
                var principal = jwtService.ValidateToken(token);
                
                if (principal != null)
                {
                    context.User = principal;
                    
                    // Log successful authentication for debugging
                    var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                    
                    _logger.LogDebug("JWT authentication successful for user {UserId} ({Email})", userId, email);
                }
                else
                {
                    _logger.LogDebug("JWT token validation failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during JWT token validation in middleware");
            }

            return Task.CompletedTask;
        }
    }

    // Extension method for easier registration
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtMiddleware>();
        }
    }
}