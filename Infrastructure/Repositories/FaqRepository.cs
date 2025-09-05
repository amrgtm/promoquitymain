using Application.DTOs.ApplicationFaqDTO;
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
    public class FaqRepository : GenericRepository<ApplicationFaq, CreateFaqDTO, UpdateFaqDTO, FaqDTO, FaqSearchDTO>, IFaqRepository
    {
        public FaqRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<FaqDTO> CreateFaqAsync(CreateFaqDTO createFaqDTO)
        {
            var id = await AddAsync(createFaqDTO);
            return await GetByIdAsync(id);
        }
        public async Task<FaqDTO> UpdateFaqAsync(UpdateFaqDTO updateFaqDTO)
        {
            var marketKPI = await GetByIdAsync(updateFaqDTO.Id);
            if (marketKPI == null)
            {
                throw new NotFoundException("Faq not found.");
            }
            await UpdateAsync(updateFaqDTO);
            return await GetByIdAsync(updateFaqDTO.Id);
        }
        public async Task<FaqDTO> DeleteFaqAsync(long id)
        {
            var marketKPI = await GetByIdAsync(id);
            if (marketKPI == null)
            {
                throw new NotFoundException("Faq not found.");
            }
            await DeleteAsync(id);
            return marketKPI;
        }

        public async Task<FaqDTO> GetFaqByIdAsync(long id)
        {
            var marketKPI = await GetByIdAsync(id);
            if (marketKPI == null)
            {
                throw new NotFoundException("Faq not found.");
            }
            return marketKPI;
        }

        public async Task<PaginatedList<FaqDTO>> GetPagedFaqListAsync(int pageIndex, int pageSize)
        {
            string sql = @"SELECT * FROM Faqs WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
           

            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<FaqDTO>(
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
