using Application.DTOs.ApplicationRolePermissionDTO;
using Application.Interfaces;
using Application.Interfaces.Default;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class RolePermissionRepository : GenericRepository<ApplicationRolePermission, CreateRolePermissionDTO, UpdateRolePermissionDTO, RolePermissionDTO, RolePermissionDTO>, IRolePermissionRepository
    {
        public RolePermissionRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<List<RolePermissionDTO>> GetPermissionByUserIdAsync(long Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", Id));
            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();
            return await QuerySqlGeneralRawAsync<RolePermissionDTO>("sp_GetUserPermissionLists", CommandType.StoredProcedure, parameterArray);
        }
    }
}
