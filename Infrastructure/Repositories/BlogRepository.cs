using Application.DTOs.ApplicationBlogDTO;
using Application.DTOs.ApplicationHomeContentDTO;
using Application.DTOs.ApplicationImageMasterDTO;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Default;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Repositories
{
    public class BlogRepository : GenericRepository<ApplicationBlog, CreateBlogDTO, UpdateBlogDTO, BlogDTO, BlogSearchDTO>, IBlogRepository
    {
        public BlogRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<BlogDTO> CreateBlogAsync(CreateBlogDTO createBlogDTO)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@BlogTitle", createBlogDTO.BlogTitle),
                new SqlParameter("@Description", createBlogDTO.BlogDesc ?? (object)DBNull.Value),
                new SqlParameter("@Image1", createBlogDTO.Image1 ?? (object)DBNull.Value),
                 new SqlParameter("@Image2", createBlogDTO.Image2 ?? (object)DBNull.Value),

                  new SqlParameter("@ImageTitle1", createBlogDTO.Image1Titile ?? (object)DBNull.Value),
                 new SqlParameter("@ImageTitle2", createBlogDTO.Image2Title ?? (object)DBNull.Value),


                new SqlParameter("@AddedBy", createBlogDTO.CreatedBy),
                new SqlParameter("@TenantId", createBlogDTO.TenantId),
            };
            var outParam = new SqlParameter("@NewId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            parameters.Add(outParam);

            await ExecuteNonQueryWithOutputAsync("sp_CreateBlogContent", CommandType.StoredProcedure, parameters.ToArray());

            long newId = (long)outParam.Value;
            return await GetBlogByIdAsync(newId);
            //
            //var blog = await FindBlogByTitle(createBlogDTO.BlogTitle);
            //if (blog != null)
            //{
            //    throw new DuplicateEntryException("Blog title already exists.");
            //}
            //var id = await AddAsync(createBlogDTO);
            //return await GetBlogByIdAsync(id);
        }
        //public async Task<BlogDTO> CreateBlogAsync(CreateBlogDTO createBlogDTO)
        //{
        //    var blog = await FindBlogByTitle(createBlogDTO.BlogTitle);
        //    if (blog != null)
        //    {
        //        throw new DuplicateEntryException("Blog title already exists.");
        //    }
        //    var id = await AddAsync(createBlogDTO);
        //    return await GetBlogByIdAsync(id);
        //}
        public async Task<BlogDTO> UpdateBlogAsync(UpdateBlogDTO updateBlogDTO)
        {
            var blog = await GetByIdAsync(updateBlogDTO.Id);
            if (blog == null)
            {
                throw new NotFoundException("Blog not found.");
            }
            await UpdateAsync(updateBlogDTO);
            return await GetBlogByIdAsync(updateBlogDTO.Id);
        }
        public async Task<BlogDTO> DeleteBlogAsync(long id)
        {
            var blog = await GetByIdAsync(id);
            if (blog == null)
            {
                throw new NotFoundException("Blog not found.");
            }
            await DeleteAsync(id);
            return blog;
        }

        public async Task<BlogDTO> GetBlogByIdAsync(long id)
        {
            var blog = await GetByIdAsync(id);//await GetByIdAsync(id, b => b.Images);//
            if (blog == null)
            {
                throw new NotFoundException("Blog not found.");
            }
            var images = await _context.Set<ApplicationImageMaster>()
                        .Where(img => img.TableId == blog.Id && img.TableName == AppConstants.Blogs)
                        .ToListAsync();
            var blogDto = _mapper.Map<BlogDTO>(blog);
            blogDto.Images = _mapper.Map<List<ImageMasterDTO>>(images);

            return blogDto;
        }

        public async Task<PaginatedList<BlogDTO>> GetPagedBlogListAsync(int pageIndex, int pageSize, string blogId = null, string blogTitle = null)
        {
            string sql = @"SELECT Id, BlogTitle, BlogDesc, Image1, Image2, Image1Titile, Image2Title, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, TenantId FROM Blogs WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(blogTitle))
            {
                sql += " AND BlogTitle LIKE '%' + @BlogTitle + '%'";
                parameters.Add(new SqlParameter("@BlogTitle", blogTitle));
            }
            if (!string.IsNullOrEmpty(blogId) && blogId != "0")
            {
                sql += " AND Id = @Id";
                parameters.Add(new SqlParameter("@Id", blogId));
            }

            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<BlogDTO>(
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

        
        private async Task<ApplicationBlog?> FindBlogByTitle(string blogTitle)
        {
            return await _context.Set<ApplicationBlog>().FirstOrDefaultAsync(b => b.BlogTitle == blogTitle);
        }
    }
}
