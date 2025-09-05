using Application.DTOs.ApplicationHomeContentDTO;
using Application.Helpers;

namespace Application.Interfaces
{
    public interface IHomeContentRepository
    {
        Task<HomeContentDTO> CreateHomeContentAsync(CreateHomeContentDTO createHomeContentDTO, string imageFile);
        Task<HomeContentDTO> UpdateHomeContentAsync(UpdateHomeContentDTO updateHomeContentDTO, string imageFile);
        Task<HomeContentDTO> DeleteHomeContentAsync(long id);
        Task<HomeContentDTO> GetHomeContentByIdAsync(long id);
        Task<PaginatedList<HomeContentDTO>> GetPagedHomeContentListAsync(int pageIndex, int pageSize, string topic=null);
    }
}
