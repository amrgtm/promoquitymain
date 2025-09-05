using Application.DTOs.ApplicationRolePermissionDTO;

namespace Application.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<List<RolePermissionDTO>> GetPermissionByUserIdAsync(long Id);
    }
}
