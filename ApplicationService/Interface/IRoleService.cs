using Application.DTOs.ApplicationRoleDTO;
using Application.Helpers;

namespace ApplicationService.Interface
{
    public interface IRoleService
    {
        Task<RoleDTO> CreateRoleAsync(CreateRoleDTO roleDTO);
        Task<RoleDTO> UpdateRoleAsync(UpdateRoleDTO roleDTO);
        Task<RoleDTO> DeleteRoleAsync(Int64 id);
        Task<RoleDTO> GetRoleByIdAsync(Int64 id);
        Task<PaginatedList<RoleDTO>> GetPagedRoleListAsync(int pageIndex, int pageSize, string roleId = null, string roleName = null);
    }
}
