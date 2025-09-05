using Application.DTOs.ApplicationFinancialKPIDTO;
using Application.Helpers;

namespace Application.Interfaces
{
    public interface IFinancialKPIRepository
    {
        Task<FinancialKPIDTO> CreateFinancialKPIAsync(CreateFinancialKPIDTO createFinancialKPIDTO);
        Task<FinancialKPIDTO> UpdateFinancialKPIAsync(UpdateFinancialKPIDTO updateFinancialKPIDTO);
        Task<FinancialKPIDTO> DeleteFinancialKPIAsync(long id);
        Task<FinancialKPIDTO> GetFinancialKPIByIdAsync(long id);
        Task<PaginatedList<FinancialKPIDTO>> GetPagedFinancialKPIListAsync(int pageIndex, int pageSize, string companyId = null);
    }
}
