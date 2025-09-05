using Application.DTOs.ApplicationRoleDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<RoleDTO> CreateRoleAsync(CreateRoleDTO roleDTO)
        {
            return _roleRepository.CreateRoleAsync(roleDTO);
        }

        public Task<RoleDTO> DeleteRoleAsync(long id)
        {
            return _roleRepository.DeleteRoleAsync(id);
        }

        public Task<PaginatedList<RoleDTO>> GetPagedRoleListAsync(int pageIndex, int pageSize, string roleId = null, string roleName = null)
        {
            return _roleRepository.GetPagedRoleListAsync(pageIndex, pageSize, roleId, roleName);
        }

        public Task<RoleDTO> GetRoleByIdAsync(long id)
        {
            return _roleRepository.GetRoleByIdAsync(id);
        }

        public Task<RoleDTO> UpdateRoleAsync(UpdateRoleDTO roleDTO)
        {
            return _roleRepository.UpdateRoleAsync(roleDTO);
        }
    }
}
