using Application.DTOs.ApplicationUserRoleDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IRoleRepository _roleRepository;
        public UserRolesService(IUserRolesRepository userRolesRepository, IRoleRepository roleRepository)
        {
            _userRolesRepository = userRolesRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserRoleDTO> CreateUserRoleAsync(CreateUserRoleDTO userRoleDTO)
        {
            var role = await _roleRepository.GetRoleByIdAsync(userRoleDTO.RoleId);
            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }
            return await _userRolesRepository.CreateUserRoleAsync(userRoleDTO);
        }
        public Task<UserRoleDTO> UpdateUserRoleAsync(UpdateUserRoleDTO userRoleDTO)
        {
            return _userRolesRepository.UpdateUserRoleAsync(userRoleDTO);
        }
        public Task<UserRoleDTO> DeleteUserRoleAsync(long id)
        {
            return _userRolesRepository.DeleteUserRoleAsync(id);
        }
        public Task<UserRoleDTO> GetUserRoleByIdAsync(long id)
        {
            return _userRolesRepository.GetUserRoleByIdAsync(id);
        }
        public Task<PaginatedList<UserRoleDTO>> GetPagedUserRoleListAsync(int pageIndex, int pageSize, string userRoleId = null)
        {
            return _userRolesRepository.GetPagedUserRoleListAsync(pageIndex, pageSize, userRoleId);
        }
    }
}
