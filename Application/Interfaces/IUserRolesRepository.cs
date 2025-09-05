
using Application.DTOs.ApplicationUserRoleDTO;
using Application.DTOs;
using Application.Helpers;

namespace Application.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<UserRoleDTO> CreateUserRoleAsync(CreateUserRoleDTO userRoleDTO);
        Task<UserRoleDTO> UpdateUserRoleAsync(UpdateUserRoleDTO userRoleDTO);
        Task<UserRoleDTO> DeleteUserRoleAsync(Int64 id);
        Task<UserRoleDTO> GetUserRoleByIdAsync(Int64 id);
        Task<PaginatedList<UserRoleDTO>> GetPagedUserRoleListAsync(int pageIndex, int pageSize, string userRoleId = null);
    }
}
