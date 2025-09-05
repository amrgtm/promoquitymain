using Application.DTOs.ApplicationMarketKPIDTO;
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
    public class MarketKPIRepository : GenericRepository<ApplicationMarketKPI, CreateMarketKPIDTO, UpdateMarketKPIDTO, MarketKPIDTO, MarketKPISearchDTO>, IMarketKPIRepository
    {
        public MarketKPIRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<MarketKPIDTO> CreateMarketKPIAsync(CreateMarketKPIDTO createMarketKPIDTO)
        {
            var id = await AddAsync(createMarketKPIDTO);
            return await GetByIdAsync(id);
        }
        public async Task<MarketKPIDTO> UpdateMarketKPIAsync(UpdateMarketKPIDTO updateMarketKPIDTO)
        {
            var marketKPI = await GetByIdAsync(updateMarketKPIDTO.Id);
            if (marketKPI == null)
            {
                throw new NotFoundException("Market KPI not found.");
            }
            await UpdateAsync(updateMarketKPIDTO);
            return await GetByIdAsync(updateMarketKPIDTO.Id);
        }
        public async Task<MarketKPIDTO> DeleteMarketKPIAsync(long id)
        {
            var marketKPI = await GetByIdAsync(id);
            if (marketKPI == null)
            {
                throw new NotFoundException("Market KPI not found.");
            }
            await DeleteAsync(id);
            return marketKPI;
        }

        public async Task<MarketKPIDTO> GetMarketKPIByIdAsync(long id)
        {
            var marketKPI = await GetByIdAsync(id);
            if (marketKPI == null)
            {
                throw new NotFoundException("Market KPI not found.");
            }
            return marketKPI;
        }

        public async Task<PaginatedList<MarketKPIDTO>> GetPagedMarketKPIListAsync(int pageIndex, int pageSize, string companyId = null)
        {
            string sql = @"SELECT Id, CompanyId, Beta, LowMarketCap, HighMarketCap, MedMarketCap, RetailSales, ActiveUsers, Volume, Duedegi, Description, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, TenantId FROM MarketKPIs WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(companyId) && companyId != "0")
            {
                sql += " AND CompanyId = @CompanyId";
                parameters.Add(new SqlParameter("@CompanyId", companyId));
            }

            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<MarketKPIDTO>(
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
