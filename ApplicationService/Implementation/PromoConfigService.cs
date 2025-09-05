using Application.DTOs.ApplicationPromoConfigDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class PromoConfigService : IPromoConfigService
    {
        private readonly IPromoConfigRepository _promoConfigRepository;
        
        public PromoConfigService(IPromoConfigRepository promoConfigRepository)
        {
            _promoConfigRepository = promoConfigRepository;
           
        }

        public async Task<PromoConfigDTO> CreatePromoConfigAsync(CreatePromoConfigDTO createPromoConfigDTO)
        {
            
            return await _promoConfigRepository.CreatePromoConfigAsync(createPromoConfigDTO);
        }

        public async Task<PromoConfigDTO> DeletePromoConfigAsync(long id)
        {
            return await _promoConfigRepository.DeletePromoConfigAsync(id);
        }

        public async Task<PromoConfigDTO> GetPromoConfigByIdAsync(long id)
        {
            return await _promoConfigRepository.GetPromoConfigByIdAsync(id);
        }

        public async Task<PaginatedList<PromoConfigDTO>> GetPagedPromoConfigListAsync(int pageIndex, int pageSize)
        {
            return await _promoConfigRepository.GetPagedPromoConfigListAsync(pageIndex, pageSize);
        }

        public async Task<PromoConfigDTO> UpdatePromoConfigAsync(UpdatePromoConfigDTO updatePromoConfigDTO)
        {
            return await _promoConfigRepository.UpdatePromoConfigAsync(updatePromoConfigDTO);
        }
    }
}
