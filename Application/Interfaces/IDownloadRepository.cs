using Application.DTOs.ApplicationDownloadDTO;
using Application.Helpers;

namespace Application.Interfaces
{
    public interface IDownloadRepository
    {
        Task<DownloadDTO> CreateDownloadInfoAsync(CreateDownloadDTO createDownloadDTO);
        Task<DownloadDTO> UpdateDownloadInfoAsync(UpdateDownloadDTO updateDownloadDTO);
        Task<DownloadDTO> DeleteDownloadInfoAsync(long id);
        Task<DownloadDTO> GetDownloadDataByIdAsync(long id);
        Task<PaginatedList<DownloadDTO>> GetPagedDownloadListAsync(int pageIndex, int pageSize, string docTitle = null, string docName = null);
    }
}
