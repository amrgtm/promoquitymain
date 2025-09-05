using Application.DTOs.ApplicationImageMasterDTO;
using Application.Helpers;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IImageMasterRepository
    {
        Task<ImageMasterDTO> CreateImageInfoAsync(CreateImageMasterDTO createImageDTO);
        Task<ImageMasterDTO> UpdateImageInfoAsync(UpdateImageMasterDTO updateImageDTO);
        Task<ImageMasterDTO> DeleteImageInfoAsync(long id);
        Task<ImageMasterDTO> GetImageInfoByIdAsync(long id);
        Task<ApplicationImageMaster?> FindImage(string imgName, string source, long tableId);
        Task<List<ApplicationImageMaster>> GetImageByTableWithId(string sourceTable, long tableId);
        Task<PaginatedList<ImageMasterDTO>> GetPagedImageListAsync(int pageIndex, int pageSize, string table = null, string tableId = null, string imageName=null);
    }
}
