using Application.DTOs.ApplicationRoleDTO;
using ApplicationCommon.CustomException;
using ApplicationCommon;
using ApplicationService.Interface;
using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleService _roleService;
        private readonly CommonMessages _messages;
        public RoleController(IRoleService roleService, ILogger<RoleController> logger, CommonMessages messages)
        {
            _roleService = roleService;
            _logger = logger;
            _messages = messages;
        }

        [HttpPost("CreateRole")]
        [HasPermission(PermissionNames.Role_Create)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO createRoleDTO)
        {
            try
            {
                var result = await _roleService.CreateRoleAsync(createRoleDTO);
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
        [HttpPut("UpdateRole")]
        [HasPermission(PermissionNames.Role_Update)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDTO updateRoleDTO)
        {
            try
            {
                var updatedRole = await _roleService.UpdateRoleAsync(updateRoleDTO);
                return Ok(ApiResponse<object>.Ok(updatedRole));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpGet("GetRoleById")]
        [HasPermission(PermissionNames.Role_Read)]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var role = await _roleService.GetRoleByIdAsync(id);
                if (role == null)
                {
                    var message = _messages.GetFriendlyMessage(CommonMessages.Mess.NotFound);
                    _logger.LogError(message);
                    return NotFound(ApiResponse<object>.Fail(message, StatusCodes.Status404NotFound));
                }
                return Ok(ApiResponse<object>.Ok(role));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpDelete("DeleteRole")]
        [HasPermission(PermissionNames.Role_Delete)]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var deletedRole = await _roleService.DeleteRoleAsync(id);
                return Ok(ApiResponse<object>.Ok(deletedRole));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpGet("GetPagedRoles")]
        [HasPermission(PermissionNames.Role_Read)]
        public async Task<IActionResult> GetPagedRoles(int pageIndex, int pageSize, string Id = null, string roleName = null)
        {
            try
            {
                var paginatedList = await _roleService.GetPagedRoleListAsync(pageIndex, pageSize, Id, roleName);
                return Ok(ApiResponse<object>.Ok(paginatedList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
    }
}
