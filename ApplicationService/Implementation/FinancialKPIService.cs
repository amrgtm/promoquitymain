using Application.DTOs.ApplicationFinancialKPIDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class FinancialKPIService : IFinancialKPIService
    {
        private readonly IFinancialKPIRepository _financialKPIRepository;
        private readonly ICompanyProfileRepository _companyProfileRepository;
        public FinancialKPIService(IFinancialKPIRepository financialKPIRepository, ICompanyProfileRepository companyProfileRepository)
        {
            _financialKPIRepository = financialKPIRepository;
            _companyProfileRepository = companyProfileRepository;
        }

        public async Task<FinancialKPIDTO> CreateFinancialKPIAsync(CreateFinancialKPIDTO createFinancialKPIDTO)
        {
            var company = await _companyProfileRepository.GetCompanyProfileByIdAsync(createFinancialKPIDTO.CompanyId);
            if (company == null)
            {
                throw new NotFoundException($"Company with Id {createFinancialKPIDTO.CompanyId} does not exist.");
            }
            return await _financialKPIRepository.CreateFinancialKPIAsync(createFinancialKPIDTO);
        }

        public async Task<FinancialKPIDTO> DeleteFinancialKPIAsync(long id)
        {
            return await _financialKPIRepository.DeleteFinancialKPIAsync(id);
        }

        public async Task<FinancialKPIDTO> GetFinancialKPIByIdAsync(long id)
        {
            return await _financialKPIRepository.GetFinancialKPIByIdAsync(id);
        }

        public async Task<PaginatedList<FinancialKPIDTO>> GetPagedFinancialKPIListAsync(int pageIndex, int pageSize, string companyId = null)
        {
            return await _financialKPIRepository.GetPagedFinancialKPIListAsync(pageIndex, pageSize, companyId);
        }

        public async Task<FinancialKPIDTO> UpdateFinancialKPIAsync(UpdateFinancialKPIDTO updateFinancialKPIDTO)
        {
            return await _financialKPIRepository.UpdateFinancialKPIAsync(updateFinancialKPIDTO);
        }
    }
}
