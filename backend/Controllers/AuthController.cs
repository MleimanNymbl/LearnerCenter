using Microsoft.AspNetCore.Mvc;
using LearnerCenter.API.Interfaces;
using LearnerCenter.API.Models.DTOs;

namespace LearnerCenter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Authenticate user with proper password verification
                var user = await _userService.AuthenticateUserAsync(loginDto.Email, loginDto.Password);

                if (user == null)
                {
                    _logger.LogWarning("Login attempt failed: Invalid credentials for {Email}", loginDto.Email);
                    return Unauthorized(new { success = false, message = "Invalid email/username or password" });
                }

                // Generate a simple token (in production, use JWT)
                var token = GenerateToken(user);

                var response = new LoginResponseDto
                {
                    Token = token,
                    User = user,
                    ExpiresAt = DateTime.UtcNow.AddHours(24).ToString("O")
                };

                _logger.LogInformation("User {Username} logged in successfully", user.Username);
                
                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login for {Email}", loginDto.Email);
                return StatusCode(500, new { success = false, message = "An error occurred during login." });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegistrationResponseDto>> Register([FromBody] UserRegistrationDto registrationDto)
        {
            try
            {
                // Delegate to the existing user registration
                var result = await _userService.RegisterUserAsync(registrationDto);
                return Ok(new { success = true, data = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during registration for {Username}", registrationDto.Username);
                return StatusCode(500, new { success = false, message = "An error occurred during registration." });
            }
        }

        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            try
            {
                // TODO: Extract user ID from JWT token
                // For demo purposes, return a placeholder response
                return Ok(new { success = false, message = "Profile endpoint not yet implemented" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving profile");
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving profile." });
            }
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            // In a real app, you'd invalidate the token
            // For now, just return success
            return Ok(new { success = true, message = "Logged out successfully" });
        }

        private string GenerateToken(UserDto user)
        {
            // TODO: Implement proper JWT token generation
            // For demo purposes, return a simple token
            var tokenData = $"{user.UserId}:{user.Username}:{DateTime.UtcNow:yyyyMMddHHmmss}";
            var tokenBytes = System.Text.Encoding.UTF8.GetBytes(tokenData);
            return Convert.ToBase64String(tokenBytes);
        }
    }
}