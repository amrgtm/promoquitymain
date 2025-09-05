using Application.DTOs.ApplicationHomeContentMidDTO;
using Application.Helpers;

namespace Application.Interfaces
{
    public interface IHomeContentMidRepository
    {
        Task<HomeContentMidDTO> CreateHomeContentMidAsync(CreateHomeContentMidDTO createHomeContentMidDTO, string imageFile);
        Task<HomeContentMidDTO> UpdateHomeContentMidAsync(UpdateHomeContentMidDTO updateHomeContentMidDTO, string imageFile);
        Task<HomeContentMidDTO> DeleteHomeContentMidAsync(long id);
        Task<HomeContentMidDTO> GetHomeContentMidByIdAsync(long id);
        Task<PaginatedList<HomeContentMidDTO>> GetPagedHomeContentMidListAsync(int pageIndex, int pageSize, string topic=null);
    }
}
