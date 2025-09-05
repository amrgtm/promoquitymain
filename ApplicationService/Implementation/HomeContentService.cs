using Application.DTOs.ApplicationHomeContentDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationCommon;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class HomeContentService : IHomeContentService
    {
        private readonly IHomeContentRepository _homeContentRepository;

        public HomeContentService(IHomeContentRepository homeContentRepository)
        {
            _homeContentRepository = homeContentRepository;
        }

        public async Task<HomeContentDTO> CreateHomeContentAsync(CreateHomeContentDTO createHomeContentDTO)
        {
            string savedImageName = string.Empty;
            if (createHomeContentDTO.File is { Length: > 0 })
            {
                savedImageName = await ImageHelper.SaveImageAsync(createHomeContentDTO.File, AppConstants.HomeContents);
            }
            return await _homeContentRepository.CreateHomeContentAsync(createHomeContentDTO, savedImageName);
        }
        public async Task<HomeContentDTO> UpdateHomeContentAsync(UpdateHomeContentDTO updateHomeContentDTO)
        {
            string savedImageName = string.Empty;
            if (updateHomeContentDTO.File is { Length: > 0 })
            {
                savedImageName = await ImageHelper.SaveImageAsync(updateHomeContentDTO.File, AppConstants.HomeContents);
            }
            return await _homeContentRepository.UpdateHomeContentAsync(updateHomeContentDTO, savedImageName);
        }
        public async Task<HomeContentDTO> DeleteHomeContentAsync(long id)
        {
            return await _homeContentRepository.DeleteHomeContentAsync(id);
        }

        public async Task<HomeContentDTO> GetHomeContentByIdAsync(long id)
        {
           return await _homeContentRepository.GetHomeContentByIdAsync(id);
        }

        public async Task<PaginatedList<HomeContentDTO>> GetPagedHomeContentListAsync(int pageIndex, int pageSize, string topic = null)
        {
            return await _homeContentRepository.GetPagedHomeContentListAsync(pageIndex, pageSize, topic);
        }

       
    }
}
