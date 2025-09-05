using Application.DTOs.ApplicationOperationalKPIDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class OperationalKPIService : IOperationalKPIService
    {
        private readonly IOperationalKPIRepository _operationalKPIRepository;
        private readonly ICompanyProfileRepository _companyProfileRepository;
        public OperationalKPIService(IOperationalKPIRepository operationalKPIRepository, ICompanyProfileRepository companyProfileRepository)
        {
            _operationalKPIRepository = operationalKPIRepository;
            _companyProfileRepository = companyProfileRepository;
        }

        public async Task<OperationalKPIDTO> CreateOperationKPIAsync(CreateOperationalKPIDTO createOperationalKPIDTO)
        {
            var company = await _companyProfileRepository.GetCompanyProfileByIdAsync(createOperationalKPIDTO.CompanyId);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company with Id {createOperationalKPIDTO.CompanyId} does not exist.");
            }
            return await _operationalKPIRepository.CreateOperationKPIAsync(createOperationalKPIDTO);
        }

        public async Task<OperationalKPIDTO> DeleteOperationKPIAsync(long id)
        {
            return await _operationalKPIRepository.DeleteOperationKPIAsync(id);
        }

        public async Task<OperationalKPIDTO> GetOperationKPIByIdAsync(long id)
        {
            return await _operationalKPIRepository.GetOperationKPIByIdAsync(id);
        }

        public async Task<PaginatedList<OperationalKPIDTO>> GetPagedOperationKPIListAsync(int pageIndex, int pageSize, string companyId = null)
        {
            return await _operationalKPIRepository.GetPagedOperationKPIListAsync(pageIndex, pageSize, companyId);
        }

        public async Task<OperationalKPIDTO> UpdateOperationKPIAsync(UpdateOperationalKPIDTO updateOperationalKPIDTO)
        {
            return await _operationalKPIRepository.UpdateOperationKPIAsync(updateOperationalKPIDTO);
        }
    }
}
