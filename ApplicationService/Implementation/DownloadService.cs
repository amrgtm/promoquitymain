using Application.DTOs.ApplicationDownloadDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class DownloadService : IDownloadService
    {
        private readonly IDownloadRepository _downloadRepository;

        public DownloadService(IDownloadRepository downloadRepository)
        {
            _downloadRepository = downloadRepository;
        }

        public async Task<DownloadDTO> CreateDownloadInfoAsync(CreateDownloadDTO createDownloadDTO)
        {
            return await _downloadRepository.CreateDownloadInfoAsync(createDownloadDTO);
        }

        public async Task<DownloadDTO> DeleteDownloadInfoAsync(long id)
        {
            return await _downloadRepository.DeleteDownloadInfoAsync(id);
        }

        public async Task<DownloadDTO> GetDownloadDataByIdAsync(long id)
        {
            return await _downloadRepository.GetDownloadDataByIdAsync(id);
        }

        public async Task<PaginatedList<DownloadDTO>> GetPagedDownloadListAsync(int pageIndex, int pageSize, string docTitle = null, string docName = null)
        {
            return await _downloadRepository.GetPagedDownloadListAsync(pageIndex, pageSize, docTitle, docName);
        }

        public async Task<DownloadDTO> UpdateDownloadInfoAsync(UpdateDownloadDTO updateDownloadDTO)
        {
            return await _downloadRepository.UpdateDownloadInfoAsync(updateDownloadDTO);
        }
    }
}
