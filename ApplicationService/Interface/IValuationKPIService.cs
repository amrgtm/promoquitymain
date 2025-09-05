using Application.DTOs.ApplicationValuationKPIDTO;
using Application.Helpers;

namespace ApplicationService.Interface
{
    public interface IValuationKPIService
    {
        Task<ValuationKPIDTO> CreateValuationKPIAsync(CreateValuationKPIDTO createValuationKPIDTO);
        Task<ValuationKPIDTO> UpdateValuationKPIAsync(UpdateValuationKPIDTO updateValuationKPIDTO);
        Task<ValuationKPIDTO> DeleteValuationKPIAsync(long id);
        Task<ValuationKPIDTO> GetValuationKPIByIdAsync(long id);
        Task<PaginatedList<ValuationKPIDTO>> GetPagedValuationKPIListAsync(int pageIndex, int pageSize, string companyId = null);
    }
}
