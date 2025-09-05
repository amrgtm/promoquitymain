using Application.DTOs.ApplicationCompanyProfileDTO;
using Application.Helpers;

namespace ApplicationService.Interface
{
    public interface ICompanyProfileService
    {
        Task<CompanyProfileDTO> CreateCompanyProfileAsync(CreateCompanyProfileDTO createCompanyDTO);
        Task<CompanyProfileDTO> UpdateCompanyProfileAsync(UpdateCompanyProfileDTO updateCompanyDTO);
        Task<CompanyProfileDTO> DeleteCompanyProfileAsync(long id);
        Task<CompanyProfileDTO> GetCompanyProfileByIdAsync(long id);
        Task<PaginatedList<CompanyProfileDTO>> GetPagedCompanyProfileListAsync(int pageIndex, int pageSize, string companyId = null, string companyName = null);
    }
}
