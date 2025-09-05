using Application.DTOs.ApplicationMarketKPIDTO;
using Application.Helpers;

namespace Application.Interfaces
{
    public interface IMarketKPIRepository
    {
        Task<MarketKPIDTO> CreateMarketKPIAsync(CreateMarketKPIDTO createMarketKPIDTO);
        Task<MarketKPIDTO> UpdateMarketKPIAsync(UpdateMarketKPIDTO updateMarketKPIDTO);
        Task<MarketKPIDTO> DeleteMarketKPIAsync(long id);
        Task<MarketKPIDTO> GetMarketKPIByIdAsync(long id);
        Task<PaginatedList<MarketKPIDTO>> GetPagedMarketKPIListAsync(int pageIndex, int pageSize, string companyId = null);
    }
}
