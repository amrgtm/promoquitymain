using Application.DTOs.ApplicationRoleDTO;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Default;
using ApplicationCommon.CustomException;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<ApplicationRole, CreateRoleDTO, UpdateRoleDTO, RoleDTO, RoleSearchDTO>, IRoleRepository
    {
        public RoleRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<RoleDTO> CreateRoleAsync(CreateRoleDTO roleDTO)
        {
            var existingRole = await FindPermissionByName(roleDTO.RoleName);
            if (existingRole != null)
            {
                throw new DuplicateEntryException("Role already exists.");
            }
            var id = await AddAsync(roleDTO);
            return await GetByIdAsync(id);
        }
        public async Task<RoleDTO> UpdateRoleAsync(UpdateRoleDTO roleDTO)
        {
            var existingRole = await GetByIdAsync(roleDTO.Id);
            if (existingRole == null)
            {
                throw new NotFoundException("Role not found.");
            }
            await UpdateAsync(roleDTO);
            return await GetByIdAsync(roleDTO.Id);
        }
        public async Task<RoleDTO> DeleteRoleAsync(long id)
        {
            var role = await GetByIdAsync(id);
            if (role == null)
            {
                throw new NotFoundException("Role not found.");
            }
            await DeleteAsync(id);
            return role;
        }

        public async Task<PaginatedList<RoleDTO>> GetPagedRoleListAsync(int pageIndex, int pageSize, string roleId = null, string roleName = null)
        {
            string sql = @"SELECT Id,RoleName,Description,CreatedBy,CreatedDate,TenantId
                            FROM [Roles] WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(roleName))
            {
                sql += " AND Name LIKE '%' + @RoleName + '%'";
                parameters.Add(new SqlParameter("@RoleName", roleName));
            }
            if (!string.IsNullOrEmpty(roleId) && roleId != "0")
            {
                sql += " AND Id = @RoleId";
                parameters.Add(new SqlParameter("@RoleId", roleId));
            }

            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<RoleDTO>(
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

        public async Task<RoleDTO> GetRoleByIdAsync(long id)
        {
            return await GetByIdAsync(id);
        }
        private async Task<ApplicationRole?> FindPermissionByName(string roleName)
        {
            return await _context.Set<ApplicationRole>().FirstOrDefaultAsync(b => b.RoleName == roleName);
        }
    }
}
