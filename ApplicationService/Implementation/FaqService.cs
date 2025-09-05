using Application.DTOs.ApplicationFaqDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class FaqService : IFaqService
    {
        private readonly IFaqRepository _faqRepository;
        
        public FaqService(IFaqRepository faqRepository)
        {
            _faqRepository = faqRepository;
           
        }

        public async Task<FaqDTO> CreateFaqAsync(CreateFaqDTO createFaqDTO)
        {
            
            return await _faqRepository.CreateFaqAsync(createFaqDTO);
        }

        public async Task<FaqDTO> DeleteFaqAsync(long id)
        {
            return await _faqRepository.DeleteFaqAsync(id);
        }

        public async Task<FaqDTO> GetFaqByIdAsync(long id)
        {
            return await _faqRepository.GetFaqByIdAsync(id);
        }

        public async Task<PaginatedList<FaqDTO>> GetPagedFaqListAsync(int pageIndex, int pageSize)
        {
            return await _faqRepository.GetPagedFaqListAsync(pageIndex, pageSize);
        }

        public async Task<FaqDTO> UpdateFaqAsync(UpdateFaqDTO updateFaqDTO)
        {
            return await _faqRepository.UpdateFaqAsync(updateFaqDTO);
        }
    }
}
