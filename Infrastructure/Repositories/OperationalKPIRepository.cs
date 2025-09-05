using Application.DTOs.ApplicationOperationalKPIDTO;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Default;
using ApplicationCommon.CustomException;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class OperationalKPIRepository : GenericRepository<ApplicationOperationalKPI, CreateOperationalKPIDTO, UpdateOperationalKPIDTO, OperationalKPIDTO, OperationalKPISearchDTO>, IOperationalKPIRepository
    {
        public OperationalKPIRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<OperationalKPIDTO> CreateOperationKPIAsync(CreateOperationalKPIDTO createOperationalKPIDTO)
        {
            var id = await AddAsync(createOperationalKPIDTO);
            return await GetByIdAsync(id);
        }
        public async Task<OperationalKPIDTO> UpdateOperationKPIAsync(UpdateOperationalKPIDTO updateOperationalKPIDTO)
        {
            var operationalKPIDTO = await GetByIdAsync(updateOperationalKPIDTO.Id);
            if (operationalKPIDTO == null)
            {
                throw new NotFoundException("Operation KPI not found.");
            }
            await UpdateAsync(updateOperationalKPIDTO);
            return await GetByIdAsync(updateOperationalKPIDTO.Id);
        }
        public async Task<OperationalKPIDTO> DeleteOperationKPIAsync(long id)
        {
            var operationalKPIDTO = await GetByIdAsync(id);
            if (operationalKPIDTO == null)
            {
                throw new NotFoundException("Operation KPI not found.");
            }
            await DeleteAsync(id);
            return operationalKPIDTO;
        }

        public async Task<OperationalKPIDTO> GetOperationKPIByIdAsync(long id)
        {
            var operationalKPIDTO = await GetByIdAsync(id);
            if (operationalKPIDTO == null)
            {
                throw new NotFoundException("Operation KPI not found.");
            }
            return operationalKPIDTO;
        }

        public async Task<PaginatedList<OperationalKPIDTO>> GetPagedOperationKPIListAsync(int pageIndex, int pageSize, string companyId = null)
        {
            string sql = @"SELECT Id, CompanyId, OperatingMargin, InvTurnOver, Description, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, TenantId FROM OperationalKPIs WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(companyId) && companyId != "0")
            {
                sql += " AND CompanyId = @CompanyId";
                parameters.Add(new SqlParameter("@CompanyId", companyId));
            }

            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<OperationalKPIDTO>(
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

       
    }
}
