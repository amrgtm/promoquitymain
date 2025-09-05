using Application.DTOs.ApplicationCompanyProfileDTO;
using Application.Helpers;

namespace Application.Interfaces
{
    public interface ICompanyProfileRepository
    {
        Task<CompanyProfileDTO> CreateCompanyProfileAsync(CreateCompanyProfileDTO createCompanyDTO,string imgName);
        Task<CompanyProfileDTO> UpdateCompanyProfileAsync(UpdateCompanyProfileDTO updateCompanyDTO, string imgName);
        Task<CompanyProfileDTO> DeleteCompanyProfileAsync(long id);
        Task<CompanyProfileDTO> GetCompanyProfileByIdAsync(long id);
        Task<PaginatedList<CompanyProfileDTO>> GetPagedCompanyProfileListAsync(int pageIndex, int pageSize, string companyId = null, string companyName = null);
    }
}
