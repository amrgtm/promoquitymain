using Application.DTOs.ApplicationPermissionDTO;
using Application.DTOs.ApplicationUserRoleDTO;
using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using ApplicationCommon.CustomException;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Application.Interfaces.Default;

namespace Infrastructure.Repositories
{
    public class UserRolesRepository : GenericRepository<ApplicationUserRole, CreateUserRoleDTO, UpdateUserRoleDTO, UserRoleDTO, UserRoleSearchDTO>, IUserRolesRepository
    {
        public UserRolesRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<UserRoleDTO> CreateUserRoleAsync(CreateUserRoleDTO userRoleDTO)
        {
            var existingRole = await FindRoleByUserId(userRoleDTO);
            if (existingRole != null)
            {
                throw new UserDefinedException("Role already assigned to this user.");
            }
            var id = await AddAsync(userRoleDTO);
            return await GetByIdAsync(id);
        }
        public async Task<UserRoleDTO> UpdateUserRoleAsync(UpdateUserRoleDTO userRoleDTO)
        {
            var existingRole = await GetByIdAsync(userRoleDTO.Id);
            if (existingRole == null)
            {
                throw new UserDefinedException("User Role not found.");
            }
            await UpdateAsync(userRoleDTO);
            return await GetByIdAsync(userRoleDTO.Id);
        }
        public async Task<UserRoleDTO> DeleteUserRoleAsync(long id)
        {
            var userRole = await GetByIdAsync(id);
            if (userRole == null)
            {
                throw new UserDefinedException("User Role not found.");
            }
            await DeleteAsync(id);
            return userRole;
        }

        public async Task<PaginatedList<UserRoleDTO>> GetPagedUserRoleListAsync(int pageIndex, int pageSize, string userRoleId = null)
        {
            string sql = @"SELECT Id,RoleId,UserId,IsGranted,CreatedBy,CreatedDate,TenantId
                            FROM [UserRoles] WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(userRoleId) && userRoleId != "0")
            {
                sql += " AND Id = @UserRoleId";
                parameters.Add(new SqlParameter("@RoleId", userRoleId));
            }
            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<UserRoleDTO>(
                sql,
                pageIndex,
                pageSize,
                orderBy: "Id",
                ascending: true,
                ComputationParams: "",
                countquery: "",
                closedbConnection: false,
                cmdType: CommandType.Text,
                parameters: parameterArray);
        }

        public async Task<UserRoleDTO> GetUserRoleByIdAsync(long id)
        {
            return await GetByIdAsync(id);
        }

        private async Task<ApplicationUserRole?> FindRoleByUserId(CreateUserRoleDTO userRoleDTO)
        {
            return await _context.Set<ApplicationUserRole>().FirstOrDefaultAsync(b => b.RoleId == userRoleDTO.RoleId && b.UserId == userRoleDTO.UserId);
        }
    }
}
