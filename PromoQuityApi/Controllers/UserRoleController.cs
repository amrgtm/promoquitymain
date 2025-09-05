using Application.DTOs.ApplicationPermissionDTO;
using Application.DTOs.ApplicationUserRoleDTO;
using ApplicationCommon.CustomException;
using ApplicationCommon;
using ApplicationService.Interface;
using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly ILogger<UserRoleController> _logger;
        private readonly IUserRolesService _userRolesService;
        private readonly CommonMessages _messages;

        public UserRoleController(ILogger<UserRoleController> logger, IUserRolesService userRolesService, CommonMessages messages)
        {
            _logger = logger;
            _userRolesService = userRolesService;
            _messages = messages;
        }
        [HttpPost("CreateUserRole")]
        [HasPermission(PermissionNames.UserRole_Create)]
        public async Task<IActionResult> CreateUserRole([FromBody] CreateUserRoleDTO createUserRoleDTO)
        {
            try
            {
                var result = await _userRolesService.CreateUserRoleAsync(createUserRoleDTO);
                return Ok(ApiResponse<object>.Ok(result));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (DuplicateEntryException)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.DuplicateEntry);
                return BadRequest(ApiResponse<object>.Fail(message, StatusCodes.Status400BadRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating role");
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }

        [HttpPut("UpdateUserRole")]
        [HasPermission(PermissionNames.UserRole_Update)]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleDTO updateUserRoleDTO)
        {
            try
            {
                var updatedUserRole = await _userRolesService.UpdateUserRoleAsync(updateUserRoleDTO);
                return Ok(ApiResponse<object>.Ok(updatedUserRole));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.UpdateFailed);
                _logger.LogError(ex, message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpGet("GetUserRoleById")]
        [HasPermission(PermissionNames.UserRole_Read)]
        public async Task<IActionResult> GetUserRoleById(int id)
        {
            try
            {
                var userRole = await _userRolesService.GetUserRoleByIdAsync(id);
                if (userRole == null)
                {
                    var message = _messages.GetFriendlyMessage(CommonMessages.Mess.NotFound);
                    return NotFound(ApiResponse<object>.Fail(message, StatusCodes.Status404NotFound));
                }
                return Ok(ApiResponse<object>.Ok(userRole));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred in GetUserRoleById with id:{id}");
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpDelete("DeleteUserRole")]
        [HasPermission(PermissionNames.UserRole_Delete)]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            try
            {
                var deletedUserRole = await _userRolesService.DeleteUserRoleAsync(id);
                return Ok(ApiResponse<object>.Ok(deletedUserRole));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred in DeleteUserRole with id:{id}");
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpGet("GetPagedUserRoles")]
        [HasPermission(PermissionNames.UserRole_Read)]
        public async Task<IActionResult> GetPagedUserRoles(int pageIndex, int pageSize, string Id = null)
        {
            try
            {
                var paginatedList = await _userRolesService.GetPagedUserRoleListAsync(pageIndex, pageSize, Id);
                return Ok(ApiResponse<object>.Ok(paginatedList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred in GetPagedUserRoles with {pageIndex}, {pageSize}, {Id}");
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
    }
}
