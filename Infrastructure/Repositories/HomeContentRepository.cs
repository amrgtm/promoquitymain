using Application.DTOs.ApplicationHomeContentDTO;
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
    public class HomeContentRepository : GenericRepository<ApplicationHomeContent, CreateHomeContentDTO, UpdateHomeContentDTO, HomeContentDTO, HomeContentSearchDTO>, IHomeContentRepository
    {
        private readonly ICurrentUserService _currentUserService;
        public HomeContentRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<HomeContentDTO> CreateHomeContentAsync(CreateHomeContentDTO createHomeContentDTO, string imageFile)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Topic", createHomeContentDTO.Topic),
                new SqlParameter("@Description", createHomeContentDTO.Description ?? (object)DBNull.Value),
                new SqlParameter("@Section", createHomeContentDTO.Section ?? (object)DBNull.Value),
                new SqlParameter("@ImageLink", imageFile ?? (object)DBNull.Value),
                new SqlParameter("@AddedBy", _currentUserService.UserId),
                new SqlParameter("@TenantId", _currentUserService.TenantId),
            };
            var outParam = new SqlParameter("@NewId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            parameters.Add(outParam);

            await ExecuteNonQueryWithOutputAsync("sp_CreateHomeContent", CommandType.StoredProcedure, parameters.ToArray());

            long newId = (long)outParam.Value;
            return await GetByIdAsync(newId);
        }
        public async Task<HomeContentDTO> UpdateHomeContentAsync(UpdateHomeContentDTO updateHomeContentDTO, string imageFile)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@HomeContentId", updateHomeContentDTO.Id),
                new SqlParameter("@Topic", updateHomeContentDTO.Topic),
                new SqlParameter("@Description", updateHomeContentDTO.Description ?? (object)DBNull.Value),
                new SqlParameter("@Section", updateHomeContentDTO.Section ?? (object)DBNull.Value),
                new SqlParameter("@ImageLink", imageFile ?? (object)DBNull.Value),
                new SqlParameter("@ModifiedBy", _currentUserService.UserId),
                new SqlParameter("@TenantId", _currentUserService.TenantId)
            };
            await ExecuteNonQueryAsync("sp_UpdateHomeContent", CommandType.StoredProcedure, parameters.ToArray());
            return await GetByIdAsync(updateHomeContentDTO.Id);
        }
        public async Task<HomeContentDTO> DeleteHomeContentAsync(long id)
        {
            var homeContent = await GetByIdAsync(id);
            if (homeContent == null)
            {
                throw new NotFoundException("Home content not found.");
            }
            await DeleteAsync(id);
            return homeContent;
        }

        public async Task<HomeContentDTO> GetHomeContentByIdAsync(long id)
        {
            var homeContent = await GetByIdAsync(id);
            if (homeContent == null)
            {
                throw new NotFoundException("Company not found.");
            }
            return homeContent;
        }

        public async Task<PaginatedList<HomeContentDTO>> GetPagedHomeContentListAsync(int pageIndex, int pageSize, string topic = null)
        {
            string sql = @"SELECT hc.Id, hc.Topic, hc.Description, img.ImageName AS ImageLink, hc.Section,
                        hc.CreatedDate, hc.ModifiedDate, hc.TenantId, hc.CreatedBy, hc.ModifiedBy 
                        FROM HomeContents hc JOIN ImageMasters img ON hc.Id = img.TableId 
                        AND img.TableName = 'HomeContents' WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();            
            if (!string.IsNullOrEmpty(topic))
            {
                sql += " AND Topic LIKE '%' + @Topic + '%'";
                parameters.Add(new SqlParameter("@Topic", topic));
            }
            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<HomeContentDTO>(
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
