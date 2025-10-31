using Microsoft.AspNetCore.Mvc;
using LearnerCenter.API.Interfaces;
using LearnerCenter.API.Models.DTOs;

namespace LearnerCenter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegistrationResponseDto>> RegisterUser([FromBody] UserRegistrationDto registrationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _userService.RegisterUserAsync(registrationDto);
                
                _logger.LogInformation("User {Username} registered successfully with ID {UserId}", 
                    result.Username, result.UserId);

                return CreatedAtAction(nameof(GetUser), new { id = result.UserId }, result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("User registration failed: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user {Username}", registrationDto.Username);
                return StatusCode(500, new { message = "An error occurred while processing your registration." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user {UserId}", id);
                return StatusCode(500, new { message = "An error occurred while retrieving the user." });
            }
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<UserDto>> GetUserByUsername(string username)
        {
            try
            {
                var user = await _userService.GetUserByUsernameAsync(username);
                
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user {Username}", username);
                return StatusCode(500, new { message = "An error occurred while retrieving the user." });
            }
        }


        [HttpGet("exists")]
        public async Task<ActionResult<UserExistsDto>> CheckUserExists([FromQuery] string username, [FromQuery] string email)
        {
            try
            {
                var result = await _userService.CheckUserExistsAsync(username, email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking user existence for {Username}/{Email}", username, email);
                return StatusCode(500, new { message = "An error occurred while checking user existence." });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSummaryDto>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all users");
                return StatusCode(500, new { message = "An error occurred while retrieving users." });
            }
        }
    }
}