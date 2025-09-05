using Application.DTOs.ApplicationCompanyProfileDTO;
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
    public class CompanyProfileRepository : GenericRepository<ApplicationCompanyProfile, CreateCompanyProfileDTO, UpdateCompanyProfileDTO, CompanyProfileDTO, CompanyProfileSearchDTO>, ICompanyProfileRepository
    {
        private readonly ICurrentUserService _currentUserService;
        public CompanyProfileRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService) : base(context, mapper, currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<CompanyProfileDTO> CreateCompanyProfileAsync(CreateCompanyProfileDTO createCompanyDTO, string imgName)
        {
            List<SqlParameter> parameters =
            [
                new SqlParameter("@CompanyName", createCompanyDTO.CompanyName),
                new SqlParameter("@Sector", createCompanyDTO.Sector ?? (object)DBNull.Value),
                new SqlParameter("@Remarks1", createCompanyDTO.Remarks1 ?? (object)DBNull.Value),
                new SqlParameter("@Remarks2", createCompanyDTO.Remarks2 ?? (object)DBNull.Value),
                new SqlParameter("@Remarks3", createCompanyDTO.Remarks3 ?? (object)DBNull.Value),
                new SqlParameter("@ImageName", imgName ?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", _currentUserService.UserId),
                new SqlParameter("@TenantId", _currentUserService.TenantId),
            ];
            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();
            var outParam = new SqlParameter("@NewId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            parameters.Add(outParam);
            await ExecuteNonQueryWithOutputAsync("sp_CreateCompanyProfile", CommandType.StoredProcedure, parameters.ToArray());
            long newId = (long)outParam.Value;
            return await GetByIdAsync(newId);
        }

        public async Task<CompanyProfileDTO> UpdateCompanyProfileAsync(UpdateCompanyProfileDTO updateCompanyDTO, string imgName)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CompanyId", updateCompanyDTO.Id),
                new SqlParameter("@CompanyName", updateCompanyDTO.CompanyName),
                new SqlParameter("@Sector", updateCompanyDTO.Sector ?? (object)DBNull.Value),
                new SqlParameter("@Remarks1", updateCompanyDTO.Remarks1 ?? (object)DBNull.Value),
                new SqlParameter("@Remarks2", updateCompanyDTO.Remarks2 ?? (object)DBNull.Value),
                new SqlParameter("@Remarks3", updateCompanyDTO.Remarks3 ?? (object)DBNull.Value),
                new SqlParameter("@ImageName", imgName ?? (object)DBNull.Value),
                new SqlParameter("@ModifiedBy", _currentUserService.UserId),
                new SqlParameter("@TenantId", _currentUserService.TenantId),
            };
            await ExecuteNonQueryAsync("sp_UpdateCompanyProfile", CommandType.StoredProcedure, parameters);
            return await GetByIdAsync(updateCompanyDTO.Id);
        }
        public async Task<CompanyProfileDTO> DeleteCompanyProfileAsync(long id)
        {
            var companyProfile = await GetByIdAsync(id);
            if (companyProfile == null)
            {
                throw new NotFoundException("Company not found.");
            }
            await DeleteAsync(id);
            return companyProfile;
        }

        public async Task<CompanyProfileDTO> GetCompanyProfileByIdAsync(long id)
        {
            var companyProfile = await GetByIdAsync(id);
            if (companyProfile == null)
            {
                throw new NotFoundException("Company not found.");
            }
            return companyProfile;
        }

        public async Task<PaginatedList<CompanyProfileDTO>> GetPagedCompanyProfileListAsync(int pageIndex, int pageSize, string companyId = null, string companyName = null)
        {
            string sql = @"SELECT cp.Id, cp.CompanyName, cp.Sector, img.ImageName AS ImageLink, cp.Remarks1, cp.Remarks2, cp.Remarks3,
                        cp.CreatedDate, cp.ModifiedDate, cp.TenantId, cp.CreatedBy, cp.ModifiedBy 
                        FROM CompanyProfiles cp JOIN ImageMasters img ON cp.Id = img.TableId 
                        AND img.TableName = 'CompanyProfiles' WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(companyId) && companyId != "0")
            {
                sql += " AND Id = @CompanyId";
                parameters.Add(new SqlParameter("@CompanyId", companyId));
            }
            if (!string.IsNullOrEmpty(companyName))
            {
                sql += " AND CompanyName LIKE '%' + @CompanyName + '%'";
                parameters.Add(new SqlParameter("@CompanyName", companyName));
            }
            var parameterArray = parameters.Select(p => new SqlParameter(p.ParameterName, p.Value)).ToArray();

            return await QuerySqlGeneralRawPaginatedAsync<CompanyProfileDTO>(
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

        private async Task<ApplicationCompanyProfile?> FindCompanyByName(string companyName)
        {
            return await _context.Set<ApplicationCompanyProfile>().FirstOrDefaultAsync(c =>c.CompanyName==companyName);
        }
    }
}
