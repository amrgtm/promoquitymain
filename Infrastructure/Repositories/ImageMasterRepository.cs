using Application.DTOs.ApplicationImageMasterDTO;
using Application.Helpers;
using Application.Interfaces;
using Application.Interfaces.Default;
using ApplicationCommon.CustomException;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Repositories
{
    public class ImageMasterRepository : GenericRepository<ApplicationImageMaster, CreateImageMasterDTO, UpdateImageMasterDTO, ImageMasterDTO, ImageMasterSearchDTO>, IImageMasterRepository
    {
        public ImageMasterRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<ImageMasterDTO> CreateImageInfoAsync(CreateImageMasterDTO createImageDTO)
        {
            var imageMaster = await FindImage(createImageDTO.ImageName,createImageDTO.Source,createImageDTO.TableId);
            if (imageMaster != null)
            {
                throw new UserDefinedException("Image already exists.");
            }
            var id = await AddAsync(createImageDTO);
            return await GetByIdAsync(id);
        }
        public async Task<ImageMasterDTO> UpdateImageInfoAsync(UpdateImageMasterDTO updateImageDTO)
        {
            var imageMaster = await GetByIdAsync(updateImageDTO.Id);
            if (imageMaster == null)
            {
                throw new UserDefinedException("Image not found.");
            }
            await UpdateAsync(updateImageDTO);
            return await GetByIdAsync(updateImageDTO.Id);
        }
        public async Task<ImageMasterDTO> DeleteImageInfoAsync(long id)
        {
            var imageMaster = await GetByIdAsync(id);
            if (imageMaster == null)
            {
                throw new UserDefinedException("Image not found.");
            }
            await DeleteAsync(id);
            return imageMaster;
        }
        public async Task<ImageMasterDTO> GetImageInfoByIdAsync(long id)
        {
            return await GetByIdAsync(id);
        }
        public async Task<PaginatedList<ImageMasterDTO>> GetPagedImageListAsync(int pageIndex, int pageSize, string table = null, string tableId = null, string imageName = null)
        {
            string sql = @"SELECT Id,TableName AS Source,ImageName, ImageTitle,IsThumbnail,TableId,CreatedBy,CreatedDate,TenantId FROM [ImageMasters] WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(table))
            {
                sql += " AND TableName LIKE '%' + @TableName + '%'";
                parameters.Add(new SqlParameter("@TableName", table));
            }
            if (!string.IsNullOrEmpty(tableId) && tableId != "0")
            {
                sql += " AND TableId = @TableId";
                parameters.Add(new SqlParameter("@TableId", tableId));
            }
            if (!string.IsNullOrEmpty(imageName))
            {
                sql += " AND ImageName LIKE '%' + @ImageName + '%'";
                parameters.Add(new SqlParameter("@ImageName", imageName));
            }
            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<ImageMasterDTO>(
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

        public async Task<ApplicationImageMaster?> FindImage(string imgName, string source, long tableId)
        {
            return await _context.Set<ApplicationImageMaster>()
                .FirstOrDefaultAsync(b =>
                    b.ImageName == imgName &&
                    b.TableName == source &&
                    (tableId == null || tableId == 0 || b.TableId == tableId)
                );
        }

        public async Task<List<ApplicationImageMaster>> GetImageByTableWithId(string sourceTable, long tableId)
        {
            return await _context.Set<ApplicationImageMaster>()
               .Where(b =>
                   b.TableName == sourceTable &&
                   (b.TableId == tableId)
               ).AsNoTracking().ToListAsync();
        }
    }
}
