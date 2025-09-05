using Application.DTOs.ApplicationFinancialKPIDTO;
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
    public class FinancialKPIRepository : GenericRepository<ApplicationFinancialKPI, CreateFinancialKPIDTO, UpdateFinancialKPIDTO, FinancialKPIDTO, FinancialKPISearchDTO>, IFinancialKPIRepository
    {
        public FinancialKPIRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<FinancialKPIDTO> CreateFinancialKPIAsync(CreateFinancialKPIDTO createFinancialKPIDTO)
        {
            var id = await AddAsync(createFinancialKPIDTO);
            return await GetByIdAsync(id);
        }
        public async Task<FinancialKPIDTO> UpdateFinancialKPIAsync(UpdateFinancialKPIDTO updateFinancialKPIDTO)
        {
            var financialKPI = await GetByIdAsync(updateFinancialKPIDTO.Id);
            if (financialKPI == null)
            {
                throw new NotFoundException("Financial KPI not found.");
            }
            await UpdateAsync(updateFinancialKPIDTO);
            return await GetByIdAsync(updateFinancialKPIDTO.Id);
        }

        public async Task<FinancialKPIDTO> DeleteFinancialKPIAsync(long id)
        {
            var financialKPI = await GetByIdAsync(id);
            if (financialKPI == null)
            {
                throw new NotFoundException("Financial KPI not found.");
            }
            await DeleteAsync(id);
            return financialKPI;
        }

        public async Task<FinancialKPIDTO> GetFinancialKPIByIdAsync(long id)
        {
            var financialKPI = await GetByIdAsync(id);
            if (financialKPI == null)
            {
                throw new NotFoundException("Financial KPI not found.");
            }
            return financialKPI;
        }

        public async Task<PaginatedList<FinancialKPIDTO>> GetPagedFinancialKPIListAsync(int pageIndex, int pageSize, string companyId = null)
        {
            string sql = @"SELECT Id, CompanyId, EPS, PERatio, ROE, ROA, NetProfitMargin, Description, CreatedDate, CreatedBy, ModifiedBy, ModifiedDate, TenantId FROM FinancialKPIs WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(companyId) && companyId != "0")
            {
                sql += " AND CompanyId = @CompanyId";
                parameters.Add(new SqlParameter("@CompanyId", companyId));
            }
            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<FinancialKPIDTO>(
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
