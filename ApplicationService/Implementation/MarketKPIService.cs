using Application.DTOs.ApplicationMarketKPIDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class MarketKPIService : IMarketKPIService
    {
        private readonly IMarketKPIRepository _marketKPIRepository;
        private readonly ICompanyProfileRepository _companyProfileRepository;
        public MarketKPIService(IMarketKPIRepository marketKPIRepository, ICompanyProfileRepository companyProfileRepository)
        {
            _marketKPIRepository = marketKPIRepository;
            _companyProfileRepository = companyProfileRepository;
        }

        public async Task<MarketKPIDTO> CreateMarketKPIAsync(CreateMarketKPIDTO createMarketKPIDTO)
        {
            var company = await _companyProfileRepository.GetCompanyProfileByIdAsync(createMarketKPIDTO.CompanyId);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company with Id {createMarketKPIDTO.CompanyId} does not exist.");
            }
            return await _marketKPIRepository.CreateMarketKPIAsync(createMarketKPIDTO);
        }

        public async Task<MarketKPIDTO> DeleteMarketKPIAsync(long id)
        {
            return await _marketKPIRepository.DeleteMarketKPIAsync(id);
        }

        public async Task<MarketKPIDTO> GetMarketKPIByIdAsync(long id)
        {
            return await _marketKPIRepository.GetMarketKPIByIdAsync(id);
        }

        public async Task<PaginatedList<MarketKPIDTO>> GetPagedMarketKPIListAsync(int pageIndex, int pageSize, string companyId = null)
        {
            return await _marketKPIRepository.GetPagedMarketKPIListAsync(pageIndex, pageSize, companyId);
        }

        public async Task<MarketKPIDTO> UpdateMarketKPIAsync(UpdateMarketKPIDTO updateMarketKPIDTO)
        {
            return await _marketKPIRepository.UpdateMarketKPIAsync(updateMarketKPIDTO);
        }
    }
}
