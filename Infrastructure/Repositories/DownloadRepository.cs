using Application.DTOs.ApplicationDownloadDTO;
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
    public class DownloadRepository : GenericRepository<ApplicationDownload, CreateDownloadDTO, UpdateDownloadDTO, DownloadDTO, DownloadSearchDTO>, IDownloadRepository
    {
        public DownloadRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
        }

        public async Task<DownloadDTO> CreateDownloadInfoAsync(CreateDownloadDTO createDownloadDTO)
        {
            var document = await FindDocument(createDownloadDTO.DocName);
            if (document != null)
            {
                throw new DuplicateEntryException("Document with same name already exists.");
            }
            var id = await AddAsync(createDownloadDTO);
            return await GetByIdAsync(id);
        }
        public Task<DownloadDTO> UpdateDownloadInfoAsync(UpdateDownloadDTO updateDownloadDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<DownloadDTO> DeleteDownloadInfoAsync(long id)
        {
            var document = await GetByIdAsync(id);
            if (document == null)
            {
                throw new NotFoundException("File not found.");
            }
            await DeleteAsync(id);
            return document;
        }

        public async Task<DownloadDTO> GetDownloadDataByIdAsync(long id)
        {
            var document = await GetByIdAsync(id);
            if (document == null)
            {
                throw new NotFoundException("File not found.");
            }
            return document;
        }

        public async Task<PaginatedList<DownloadDTO>> GetPagedDownloadListAsync(int pageIndex, int pageSize, string docTitle = null, string docName = null)
        {
            string sql = @"SELECT Id, DocTitle, DocDesc, DocName, IsVisible, Ordering, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, TenantId FROM Downloads WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(docTitle))
            {
                sql += " AND DocTitle LIKE '%' + @DocTitle + '%'";
                parameters.Add(new SqlParameter("@DocTitle", docTitle));
            }
            if (!string.IsNullOrEmpty(docName) && docName != "0")
            {
                sql += " AND DocName = @DocName";
                parameters.Add(new SqlParameter("@DocName", docName));
            }

            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<DownloadDTO>(
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

        private async Task<ApplicationDownload?> FindDocument(string docName)
        {
            return await _context.Set<ApplicationDownload>().FirstOrDefaultAsync(b => b.DocName == docName);
        }
    }
}
