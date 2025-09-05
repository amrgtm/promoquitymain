using Application.DTOs.ApplicationCompanyProfileDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationCommon;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class CompanyProfileService : ICompanyProfileService
    {
        private readonly ICompanyProfileRepository _companyProfileRepository;
        private readonly IImageMasterRepository _imageMasterRepository;
        public CompanyProfileService(ICompanyProfileRepository companyProfileRepository, IImageMasterRepository imageMasterRepository)
        {
            _companyProfileRepository = companyProfileRepository;
            _imageMasterRepository = imageMasterRepository;
        }

        public async Task<CompanyProfileDTO> CreateCompanyProfileAsync(CreateCompanyProfileDTO createCompanyDTO)
        {
            string savedImageName = string.Empty;
            if (createCompanyDTO.File is { Length: > 0 })
            {
                savedImageName = await ImageHelper.SaveImageAsync(createCompanyDTO.File, AppConstants.CompanyProfiles);
            }
            return await _companyProfileRepository.CreateCompanyProfileAsync(createCompanyDTO, savedImageName);
        }

        public async Task<CompanyProfileDTO> DeleteCompanyProfileAsync(long id)
        {
            return await _companyProfileRepository.DeleteCompanyProfileAsync(id);
        }

        public async Task<CompanyProfileDTO> GetCompanyProfileByIdAsync(long id)
        {
            return await _companyProfileRepository.GetCompanyProfileByIdAsync(id);
        }

        public async Task<PaginatedList<CompanyProfileDTO>> GetPagedCompanyProfileListAsync(int pageIndex, int pageSize, string companyId = null, string companyName = null)
        {
            return await _companyProfileRepository.GetPagedCompanyProfileListAsync(pageIndex, pageSize, companyId, companyName);
        }

        public async Task<CompanyProfileDTO> UpdateCompanyProfileAsync(UpdateCompanyProfileDTO updateCompanyDTO)
        {
            string savedImageName = string.Empty;
            if (updateCompanyDTO.File is { Length: > 0 })
            {
                savedImageName = await ImageHelper.SaveImageAsync(updateCompanyDTO.File, AppConstants.CompanyProfiles);
            }
            return await _companyProfileRepository.UpdateCompanyProfileAsync(updateCompanyDTO, savedImageName);
        }
    }
}
