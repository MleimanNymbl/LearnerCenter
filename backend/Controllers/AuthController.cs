using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LearnerCenter.API.Interfaces;
using LearnerCenter.API.Models.DTOs;
using LearnerCenter.API.Services;
using System.Security.Claims;

namespace LearnerCenter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, IJwtService jwtService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _jwtService = jwtService;
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

                // Generate JWT token
                var token = _jwtService.GenerateToken(user);

                var response = new LoginResponseDto
                {
                    Token = token,
                    User = user,
                    ExpiresAt = DateTime.UtcNow.AddHours(1).ToString("O") // JWT expiry from config
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
        [Authorize]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            try
            {
                // Extract user ID from JWT token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { success = false, message = "Invalid or missing user authentication." });
                }

                var user = await _userService.GetUserByIdAsync(userId);
                
                if (user == null)
                {
                    return NotFound(new { success = false, message = "User not found." });
                }

                return Ok(new { success = true, data = user });
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
            // With JWT tokens, logout is handled client-side by removing the token
            // In production, you might want to implement token blacklisting
            return Ok(new { success = true, message = "Logged out successfully" });
        }
    }
}