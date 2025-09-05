using Application.DTOs.ApplicationUserDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly CommonMessages _messages;
        private readonly ILogger<UserController> _logger;
        private readonly IUserRolesService _userRolesService;
        public UserController(IUserService userService,
            IUserRolesService userRolesService,



            CommonMessages messages, ILogger<UserController> logger)
        {
            _userService = userService;
            _messages = messages;
            _logger = logger;
            _userRolesService = userRolesService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var loginResponse = await _userService.LoginUserAsync(loginDTO);
                return Ok(ApiResponse<object>.Ok(loginResponse));
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access attempt.");
                return Unauthorized(ApiResponse<object>.Fail("Invalid credentials", StatusCodes.Status401Unauthorized));
            }
            catch (Exception ex)
            {
                var message = "An error occurred during login.";
                _logger.LogError(ex, message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<object>.Fail(message, StatusCodes.Status500InternalServerError));
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(createUserDTO);
                return Ok(ApiResponse<UserDTO>.Ok(user, _messages.GetFriendlyMessage(CommonMessages.Mess.CreateSucceeded)));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (DuplicateEntryException ex)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.DuplicateEntry);
                _logger.LogError(ex, message);
                return Conflict(ApiResponse<object>.Fail(message, StatusCodes.Status409Conflict));
            }
            catch (Exception ex)
            {
                var message = "An error occurred during registration.";
                _logger.LogError(ex, message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<object>.Fail(message, StatusCodes.Status500InternalServerError));
            }
        }

        [HttpGet("userprofile")]
        public async Task<IActionResult> GetUserProfile(long? userCode, string emailId = null, string userName = null)
        {
            try
            {
                var user = await _userService.GetUserProfileAsync(userCode, emailId, userName);
                if (user == null)
                {
                    var message = _messages.GetFriendlyMessage(CommonMessages.Mess.NotFound);
                    return NotFound(ApiResponse<object>.Fail(message, StatusCodes.Status404NotFound));
                }
                return Ok(ApiResponse<object>.Ok(user));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                var message = "An error occurred while retrieving the user profile.";
                _logger.LogError(ex, message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<object>.Fail(message, StatusCodes.Status500InternalServerError));
            }
        }

    }
}
