using Application.DTOs.ApplicationRoleDTO;
using Application.Helpers;

namespace Application.Interfaces
{
    public interface IRoleRepository
    {
        Task<RoleDTO> CreateRoleAsync(CreateRoleDTO roleDTO);
        Task<RoleDTO> UpdateRoleAsync(UpdateRoleDTO roleDTO);
        Task<RoleDTO> DeleteRoleAsync(long id);
        Task<RoleDTO> GetRoleByIdAsync(long id);
        Task<PaginatedList<RoleDTO>> GetPagedRoleListAsync(int pageIndex, int pageSize, string roleId = null, string roleName = null);
    }
}
