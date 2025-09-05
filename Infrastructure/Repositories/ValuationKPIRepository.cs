using Application.DTOs.ApplicationValuationKPIDTO;
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
    public class ValuationKPIRepository : GenericRepository<ApplicationValuationKPI, CreateValuationKPIDTO, UpdateValuationKPIDTO, ValuationKPIDTO, ValuationKPISearchDTO>, IValuationKPIRepository
    {
        public ValuationKPIRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<ValuationKPIDTO> CreateValuationKPIAsync(CreateValuationKPIDTO createValuationKPIDTO)
        {
            var id = await AddAsync(createValuationKPIDTO);
            return await GetByIdAsync(id);
        }
        public async Task<ValuationKPIDTO> UpdateValuationKPIAsync(UpdateValuationKPIDTO updateValuationKPIDTO)
        {
            var valuationKPI = await GetByIdAsync(updateValuationKPIDTO.Id);
            if (valuationKPI == null)
            {
                throw new NotFoundException("Market KPI not found.");
            }
            await UpdateAsync(updateValuationKPIDTO);
            return await GetByIdAsync(updateValuationKPIDTO.Id);
        }
        public async Task<ValuationKPIDTO> DeleteValuationKPIAsync(long id)
        {
            var valuationKPI = await GetByIdAsync(id);
            if (valuationKPI == null)
            {
                throw new NotFoundException("Market KPI not found.");
            }
            await DeleteAsync(id);
            return valuationKPI;
        }

        public async Task<PaginatedList<ValuationKPIDTO>> GetPagedValuationKPIListAsync(int pageIndex, int pageSize, string companyId = null)
        {
            string sql = @"SELECT Id, CompanyId, PBRatio, DividendYield, Ebitda, Description, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, TenantId FROM ValuationKPIs WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(companyId) && companyId != "0")
            {
                sql += " AND CompanyId = @CompanyId";
                parameters.Add(new SqlParameter("@CompanyId", companyId));
            }
            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<ValuationKPIDTO>(
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

        public async Task<ValuationKPIDTO> GetValuationKPIByIdAsync(long id)
        {
            var valuationKPI = await GetByIdAsync(id);
            if (valuationKPI == null)
            {
                throw new NotFoundException("Market KPI not found.");
            }
            return valuationKPI;
        }
    }
}
