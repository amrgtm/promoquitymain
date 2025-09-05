using Application.DTOs.ApplicationHomeContentMidDTO;
using Application.Helpers;

namespace ApplicationService.Interface
{
    public interface IHomeContentMidService
    {
        Task<HomeContentMidDTO> CreateHomeContentMidAsync(CreateHomeContentMidDTO createHomeContentMidDTO);
        Task<HomeContentMidDTO> UpdateHomeContentMidAsync(UpdateHomeContentMidDTO updateHomeContentMidDTO);
        Task<HomeContentMidDTO> DeleteHomeContentMidAsync(long id);
        Task<HomeContentMidDTO> GetHomeContentMidByIdAsync(long id);
        Task<PaginatedList<HomeContentMidDTO>> GetPagedHomeContentMidListAsync(int pageIndex, int pageSize, string topic = null);
    }
}
