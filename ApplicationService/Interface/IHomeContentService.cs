using Application.DTOs.ApplicationHomeContentDTO;
using Application.Helpers;

namespace ApplicationService.Interface
{
    public interface IHomeContentService
    {
        Task<HomeContentDTO> CreateHomeContentAsync(CreateHomeContentDTO createHomeContentDTO);
        Task<HomeContentDTO> UpdateHomeContentAsync(UpdateHomeContentDTO updateHomeContentDTO);
        Task<HomeContentDTO> DeleteHomeContentAsync(long id);
        Task<HomeContentDTO> GetHomeContentByIdAsync(long id);
        Task<PaginatedList<HomeContentDTO>> GetPagedHomeContentListAsync(int pageIndex, int pageSize, string topic = null);
    }
}
