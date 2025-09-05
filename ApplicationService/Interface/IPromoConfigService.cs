using Application.DTOs.ApplicationPromoConfigDTO;
using Application.Helpers;

namespace ApplicationService.Interface
{
    public interface IPromoConfigService
    {
        Task<PromoConfigDTO> CreatePromoConfigAsync(CreatePromoConfigDTO createPromoConfigDTO);
        Task<PromoConfigDTO> UpdatePromoConfigAsync(UpdatePromoConfigDTO updatePromoConfigDTO);
        Task<PromoConfigDTO> DeletePromoConfigAsync(long id);
        Task<PromoConfigDTO> GetPromoConfigByIdAsync(long id);
        Task<PaginatedList<PromoConfigDTO>> GetPagedPromoConfigListAsync(int pageIndex, int pageSize);
    }
}
