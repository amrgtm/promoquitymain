using Application.DTOs.ApplicationImageMasterDTO;
using Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace ApplicationService.Interface
{
    public interface IImageMasterService
    {
        Task<ImageMasterDTO> CreateImageInfoAsync(CreateImageMasterDTO createImageDTO);
        Task<ImageMasterDTO> UpdateImageInfoAsync(UpdateImageMasterDTO updateImageDTO);
        Task<ImageMasterDTO> DeleteImageInfoAsync(long id);
        Task<ImageMasterDTO> GetImageInfoByIdAsync(long id);
        Task<PaginatedList<ImageMasterDTO>> GetPagedImageListAsync(int pageIndex, int pageSize, string table = null, string tableId = null, string imageName = null);
    }
}
