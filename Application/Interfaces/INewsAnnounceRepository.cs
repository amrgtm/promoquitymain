using Application.DTOs.ApplicationNewsAnnounceDTO;
using Application.Helpers;

namespace Application.Interfaces
{
    public interface INewsAnnounceRepository
    {
        Task<NewsAnnounceDTO> CreateNewsAsync(CreateNewsAnnounceDTO createHomeContentDTO);
        Task<NewsAnnounceDTO> UpdateNewsAsync(UpdateNewsAnnounceDTO updateHomeContentDTO);
        Task<NewsAnnounceDTO> DeleteNewsAsync(long id);
        Task<NewsAnnounceDTO> GetNewsByIdAsync(long id);
        Task<PaginatedList<NewsAnnounceDTO>> GetPagedNewsListAsync(int pageIndex, int pageSize, string title = null);
    }
}
