using Application.DTOs.ApplicationBlogDTO;
using Application.DTOs.ApplicationImageMasterDTO;
using Application.DTOs.ApplicationNewsAnnounceDTO;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Default;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Repositories
{
    public class NewsAnnounceRepository : GenericRepository<ApplicationNewsAnnounce, CreateNewsAnnounceDTO, UpdateNewsAnnounceDTO, NewsAnnounceDTO, NewsAnnounceSearchDTO>, INewsAnnounceRepository
    {
        public NewsAnnounceRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<NewsAnnounceDTO> CreateNewsAsync(CreateNewsAnnounceDTO createHomeContentDTO)
        {
            //var news = await FindNewsByTitle(createHomeContentDTO.Title);
            //if (news != null)
            //{
            //    throw new UserDefinedException("News already exists");
            //}


            //
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Title", createHomeContentDTO.Title),
                new SqlParameter("@Description", createHomeContentDTO.Description ?? (object)DBNull.Value),
                new SqlParameter("@Image1", createHomeContentDTO.Image1 ?? (object)DBNull.Value),
                new SqlParameter("@Image2", createHomeContentDTO.Image2 ?? (object)DBNull.Value),
                new SqlParameter("@ImageTitle1", createHomeContentDTO.Image1Title ?? (object)DBNull.Value),
                new SqlParameter("@ImageTitle2", createHomeContentDTO.Image2Title ?? (object)DBNull.Value),
                new SqlParameter("@AddedBy", createHomeContentDTO.CreatedBy),
                new SqlParameter("@TenantId", createHomeContentDTO.TenantId),
                new SqlParameter("@Ordering", createHomeContentDTO.Ordering.ToString() ?? (object)DBNull.Value),
                new SqlParameter("@Source", createHomeContentDTO.Source ?? (object)DBNull.Value),
                new SqlParameter("@ReadDuration", createHomeContentDTO.ReadDuration ?? (object)DBNull.Value),
                new SqlParameter("@Visit", createHomeContentDTO.Visit.ToString() ?? (object)DBNull.Value),                
                new SqlParameter("@Priority", createHomeContentDTO.Priority.ToString() ?? (object)DBNull.Value),
                new SqlParameter("@PublishedDate", createHomeContentDTO.PublishedDate.ToString() ?? (object)DBNull.Value),
            };
            var outParam = new SqlParameter("@NewId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            parameters.Add(outParam);

            await ExecuteNonQueryWithOutputAsync("sp_CreateNewsContent", CommandType.StoredProcedure, parameters.ToArray());

            long newId = (long)outParam.Value;

            return await GetByIdAsync(newId);
        }
        public async Task<NewsAnnounceDTO> GetNewsByIdAsync(long id)
        {
            var blog = await GetByIdAsync(id);//await GetByIdAsync(id, b => b.Images);//
            if (blog == null)
            {
                throw new NotFoundException("News not found.");
            }
            var images = await _context.Set<ApplicationImageMaster>()
                        .Where(img => img.TableId == blog.Id && img.TableName == AppConstants.NewsContents)
                        .ToListAsync();
            var blogDto = _mapper.Map<NewsAnnounceDTO>(blog);
            blogDto.Images = _mapper.Map<List<ImageMasterDTO>>(images);

            return blog;
        }
        public async Task<NewsAnnounceDTO> DeleteNewsAsync(long id)
        {
            var news = await GetByIdAsync(id);
            if (news == null)
            {
                throw new NotFoundException("News not found.");
            }
            await DeleteAsync(id);
            return news;
        }

     
        public async Task<PaginatedList<NewsAnnounceDTO>> GetPagedNewsListAsync(int pageIndex, int pageSize, string title = null)
        {
            string sql = @"SELECT Id, Title, Description, Image1, Image2, Image1Title, Image2Title, PublishedDate, Ordering, Source, ReadDuration, Visit, Priority, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, TenantId 
                         FROM NewsAnnounces  WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(title))
            {
                sql += " AND Title LIKE '%' + @Title + '%'";
                parameters.Add(new SqlParameter("@Title", title));
            }
            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<NewsAnnounceDTO>(
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

        public async Task<NewsAnnounceDTO> UpdateNewsAsync(UpdateNewsAnnounceDTO updateHomeContentDTO)
        {
            var news = await GetByIdAsync(updateHomeContentDTO.Id);
            if (news == null)
            {
                throw new NotFoundException("News not found.");
            }
            await UpdateAsync(updateHomeContentDTO);
            return await GetByIdAsync(news.Id);
        }
        private async Task<ApplicationNewsAnnounce?> FindNewsByTitle(string title)
        {
            return await _context.Set<ApplicationNewsAnnounce>().FirstOrDefaultAsync(b => b.Title == title);
        }
    }

}
