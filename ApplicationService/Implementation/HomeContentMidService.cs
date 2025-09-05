using Application.DTOs.ApplicationHomeContentMidDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationCommon;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class HomeContentMidService : IHomeContentMidService
    {
        private readonly IHomeContentMidRepository _homeContentRepository;

        public HomeContentMidService(IHomeContentMidRepository homeContentRepository)
        {
            _homeContentRepository = homeContentRepository;
        }

        public async Task<HomeContentMidDTO> CreateHomeContentMidAsync(CreateHomeContentMidDTO createHomeContentMidDTO)
        {
            string savedImageName = string.Empty;
            if (createHomeContentMidDTO.File is { Length: > 0 })
            {
                savedImageName = await ImageHelper.SaveImageAsync(createHomeContentMidDTO.File, AppConstants.HomeContentMids);
            }
            return await _homeContentRepository.CreateHomeContentMidAsync(createHomeContentMidDTO, savedImageName);
        }
        public async Task<HomeContentMidDTO> UpdateHomeContentMidAsync(UpdateHomeContentMidDTO updateHomeContentMidDTO)
        {
            string savedImageName = string.Empty;
            if (updateHomeContentMidDTO.File is { Length: > 0 })
            {
                savedImageName = await ImageHelper.SaveImageAsync(updateHomeContentMidDTO.File, AppConstants.HomeContentMids);
            }
            return await _homeContentRepository.UpdateHomeContentMidAsync(updateHomeContentMidDTO, savedImageName);
        }
        public async Task<HomeContentMidDTO> DeleteHomeContentMidAsync(long id)
        {
            return await _homeContentRepository.DeleteHomeContentMidAsync(id);
        }

        public async Task<HomeContentMidDTO> GetHomeContentMidByIdAsync(long id)
        {
           return await _homeContentRepository.GetHomeContentMidByIdAsync(id);
        }

        public async Task<PaginatedList<HomeContentMidDTO>> GetPagedHomeContentMidListAsync(int pageIndex, int pageSize, string topic = null)
        {
            return await _homeContentRepository.GetPagedHomeContentMidListAsync(pageIndex, pageSize, topic);
        }

       
    }
}
