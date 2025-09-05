using Application.DTOs.ApplicationImageMasterDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Http;

namespace ApplicationService.Implementation
{
    public class ImageMasterService : IImageMasterService
    {
        private readonly IImageMasterRepository _imageMasterRepository;

        public ImageMasterService(IImageMasterRepository imageMasterRepository)
        {
            _imageMasterRepository = imageMasterRepository;
        }

        public async Task<ImageMasterDTO> CreateImageInfoAsync(CreateImageMasterDTO createImageDTO)
        {
            if (createImageDTO.File != null && createImageDTO.File.Length > 0)
            {
                createImageDTO.Source = GetImageSource(createImageDTO.Source);
                var savedFileName = await ImageHelper.SaveImageAsync(createImageDTO.File, createImageDTO.Source);
                createImageDTO.ImageName = savedFileName;
            }
            var exisingImges = await _imageMasterRepository.GetImageByTableWithId(createImageDTO.Source, createImageDTO.TableId);
            if (exisingImges.Count > 0)
            {
                var exisingImg = exisingImges.FirstOrDefault(x => x.IsThumbnail == createImageDTO.IsThumbnail);
                if (exisingImg != null)
                {
                    // Delete the existing image file
                    await _imageMasterRepository.DeleteImageInfoAsync(exisingImg.Id);
                }
            }
            return await _imageMasterRepository.CreateImageInfoAsync(createImageDTO);
        }

        public async Task<ImageMasterDTO> DeleteImageInfoAsync(long id)
        {
            return await _imageMasterRepository.DeleteImageInfoAsync(id);
        }

        public async Task<ImageMasterDTO> GetImageInfoByIdAsync(long id)
        {
            var imageInfo = await _imageMasterRepository.GetImageInfoByIdAsync(id);
            if (imageInfo == null)
            {
                throw new NotFoundException($"Image with ID {id} not found.");
            }
            return imageInfo;
        }

        public async Task<PaginatedList<ImageMasterDTO>> GetPagedImageListAsync(int pageIndex, int pageSize, string table = null, string tableId = null, string imageName = null)
        {
            return await _imageMasterRepository.GetPagedImageListAsync(pageIndex, pageSize, table, tableId, imageName);
        }

        public async Task<ImageMasterDTO> UpdateImageInfoAsync(UpdateImageMasterDTO updateImageDTO)
        {
            if (updateImageDTO.File != null && updateImageDTO.File.Length > 0)
            {
                updateImageDTO.Source = GetImageSource(updateImageDTO.Source);
                var savedFileName = await ImageHelper.SaveImageAsync(updateImageDTO.File, updateImageDTO.Source);
                updateImageDTO.ImageName = savedFileName;                
            }
            return await _imageMasterRepository.UpdateImageInfoAsync(updateImageDTO);
        }

        private string GetImageSource(string key)
        {
            var sources = AppConstants.ImageSources.FirstOrDefault(s => s["Key"].Equals(key, StringComparison.OrdinalIgnoreCase));
            return sources != null ? sources["Value"] : key;//AppConstants.Unknown;
        }
    }
}
