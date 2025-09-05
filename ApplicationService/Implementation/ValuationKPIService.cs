using Application.DTOs.ApplicationValuationKPIDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class ValuationKPIService : IValuationKPIService
    {
        private readonly IValuationKPIRepository _valuationKPIRepository;
        private readonly ICompanyProfileRepository _companyProfileRepository;
        public ValuationKPIService(IValuationKPIRepository valuationKPIRepository, ICompanyProfileRepository companyProfileRepository)
        {
            _valuationKPIRepository = valuationKPIRepository;
            _companyProfileRepository = companyProfileRepository;
        }

        public async Task<ValuationKPIDTO> CreateValuationKPIAsync(CreateValuationKPIDTO createValuationKPIDTO)
        {
            var company = await _companyProfileRepository.GetCompanyProfileByIdAsync(createValuationKPIDTO.CompanyId);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company with Id {createValuationKPIDTO.CompanyId} does not exist.");
            }
            return await _valuationKPIRepository.CreateValuationKPIAsync(createValuationKPIDTO);
        }

        public async Task<ValuationKPIDTO> DeleteValuationKPIAsync(long id)
        {
            return await _valuationKPIRepository.DeleteValuationKPIAsync(id);
        }

        public async Task<PaginatedList<ValuationKPIDTO>> GetPagedValuationKPIListAsync(int pageIndex, int pageSize, string companyId = null)
        {
            return await _valuationKPIRepository.GetPagedValuationKPIListAsync(pageIndex, pageSize, companyId);
        }

        public async Task<ValuationKPIDTO> GetValuationKPIByIdAsync(long id)
        {
            return await _valuationKPIRepository.GetValuationKPIByIdAsync(id);
        }

        public async Task<ValuationKPIDTO> UpdateValuationKPIAsync(UpdateValuationKPIDTO updateValuationKPIDTO)
        {
            return await _valuationKPIRepository.UpdateValuationKPIAsync(updateValuationKPIDTO);
        }
    }
}
