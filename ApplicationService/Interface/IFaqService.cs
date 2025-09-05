using Application.DTOs.ApplicationFaqDTO;
using Application.Helpers;

namespace ApplicationService.Interface
{
    public interface IFaqService
    {
        Task<FaqDTO> CreateFaqAsync(CreateFaqDTO createFaqDTO);
        Task<FaqDTO> UpdateFaqAsync(UpdateFaqDTO updateFaqDTO);
        Task<FaqDTO> DeleteFaqAsync(long id);
        Task<FaqDTO> GetFaqByIdAsync(long id);
        Task<PaginatedList<FaqDTO>> GetPagedFaqListAsync(int pageIndex, int pageSize);
    }
}
