using Application.DTOs.ApplicationPromoConfigDTO;
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
    public class PromoConfigRepository : GenericRepository<ApplicationPromoConfig, CreatePromoConfigDTO, UpdatePromoConfigDTO, PromoConfigDTO, PromoConfigSearchDTO>, IPromoConfigRepository
    {
        public PromoConfigRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<PromoConfigDTO> CreatePromoConfigAsync(CreatePromoConfigDTO createPromoConfigDTO)
        {
            var id = await AddAsync(createPromoConfigDTO);
            return await GetByIdAsync(id);
        }
        public async Task<PromoConfigDTO> UpdatePromoConfigAsync(UpdatePromoConfigDTO updatePromoConfigDTO)
        {
            var marketKPI = await GetByIdAsync(updatePromoConfigDTO.Id);
            if (marketKPI == null)
            {
                throw new NotFoundException(" Promo config not found.");
            }
            await UpdateAsync(updatePromoConfigDTO);
            return await GetByIdAsync(updatePromoConfigDTO.Id);
        }
        public async Task<PromoConfigDTO> DeletePromoConfigAsync(long id)
        {
            var marketKPI = await GetByIdAsync(id);
            if (marketKPI == null)
            {
                throw new NotFoundException(" Promo config not found.");
            }
            await DeleteAsync(id);
            return marketKPI;
        }

        public async Task<PromoConfigDTO> GetPromoConfigByIdAsync(long id)
        {
            var marketKPI = await GetByIdAsync(id);
            if (marketKPI == null)
            {
                throw new NotFoundException(" Promo config not found.");
            }
            return marketKPI;
        }

        public async Task<PaginatedList<PromoConfigDTO>> GetPagedPromoConfigListAsync(int pageIndex, int pageSize)
        {
            string sql = @"SELECT * FROM PromoConfigs WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
            

            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<PromoConfigDTO>(
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
