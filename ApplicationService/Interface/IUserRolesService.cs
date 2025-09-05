using Application.DTOs.ApplicationPermissionDTO;
using Application.DTOs.ApplicationUserRoleDTO;
using Application.DTOs;
using Application.Helpers;

namespace ApplicationService.Interface
{
    public interface IUserRolesService
    {
        Task<UserRoleDTO> CreateUserRoleAsync(CreateUserRoleDTO userRoleDTO);
        Task<UserRoleDTO> UpdateUserRoleAsync(UpdateUserRoleDTO userRoleDTO);
        Task<UserRoleDTO> DeleteUserRoleAsync(Int64 id);
        Task<UserRoleDTO> GetUserRoleByIdAsync(Int64 id);
        Task<PaginatedList<UserRoleDTO>> GetPagedUserRoleListAsync(int pageIndex, int pageSize, string userRoleId = null);
    }
}
