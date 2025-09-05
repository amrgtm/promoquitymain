using Application.DTOs.ApplicationHomeContentMidDTO;
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
    public class HomeContentMidRepository : GenericRepository<ApplicationHomeContentMid, CreateHomeContentMidDTO, UpdateHomeContentMidDTO, HomeContentMidDTO, HomeContentSearchMidDTO>, IHomeContentMidRepository
    {
        private readonly ICurrentUserService _currentUserService;
        public HomeContentMidRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<HomeContentMidDTO> CreateHomeContentMidAsync(CreateHomeContentMidDTO createHomeContentMidDTO, string imageFile)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Topic", createHomeContentMidDTO.Topic),
                new SqlParameter("@Description", createHomeContentMidDTO.Description ?? (object)DBNull.Value),
                new SqlParameter("@Section", createHomeContentMidDTO.Section ?? (object)DBNull.Value),
                new SqlParameter("@ImageLink", imageFile ?? (object)DBNull.Value),
                new SqlParameter("@AddedBy", _currentUserService.UserId),
                new SqlParameter("@TenantId", _currentUserService.TenantId),
            };
            var outParam = new SqlParameter("@NewId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            parameters.Add(outParam);

            await ExecuteNonQueryWithOutputAsync("sp_CreateHomeContentMid", CommandType.StoredProcedure, parameters.ToArray());

            long newId = (long)outParam.Value;
            return await GetByIdAsync(newId);
        }
        public async Task<HomeContentMidDTO> UpdateHomeContentMidAsync(UpdateHomeContentMidDTO updateHomeContentMidDTO, string imageFile)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@HomeContentMidId", updateHomeContentMidDTO.Id),
                new SqlParameter("@Topic", updateHomeContentMidDTO.Topic),
                new SqlParameter("@Description", updateHomeContentMidDTO.Description ?? (object)DBNull.Value),
                new SqlParameter("@Section", updateHomeContentMidDTO.Section ?? (object)DBNull.Value),
                new SqlParameter("@ImageLink", imageFile ?? (object)DBNull.Value),
                new SqlParameter("@ModifiedBy", _currentUserService.UserId),
                new SqlParameter("@TenantId", _currentUserService.TenantId)
            };
            await ExecuteNonQueryAsync("sp_UpdateHomeContentMid", CommandType.StoredProcedure, parameters.ToArray());
            return await GetByIdAsync(updateHomeContentMidDTO.Id);
        }
        public async Task<HomeContentMidDTO> DeleteHomeContentMidAsync(long id)
        {
            var homeContent = await GetByIdAsync(id);
            if (homeContent == null)
            {
                throw new NotFoundException("Home content not found.");
            }
            await DeleteAsync(id);
            return homeContent;
        }

        public async Task<HomeContentMidDTO> GetHomeContentMidByIdAsync(long id)
        {
            var homeContent = await GetByIdAsync(id);
            if (homeContent == null)
            {
                throw new NotFoundException("Company not found.");
            }
            return homeContent;
        }

        public async Task<PaginatedList<HomeContentMidDTO>> GetPagedHomeContentMidListAsync(int pageIndex, int pageSize, string topic = null)
        {
            string sql = @"SELECT hc.Id, hc.Topic, hc.Description, img.ImageName AS ImageLink, hc.Section,
                        hc.CreatedDate, hc.ModifiedDate, hc.TenantId, hc.CreatedBy, hc.ModifiedBy 
                        FROM HomeContentMids hc JOIN ImageMasters img ON hc.Id = img.TableId 
                        AND img.TableName = 'HomeContentMids' WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();            
            if (!string.IsNullOrEmpty(topic))
            {
                sql += " AND Topic LIKE '%' + @Topic + '%'";
                parameters.Add(new SqlParameter("@Topic", topic));
            }
            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<HomeContentMidDTO>(
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
