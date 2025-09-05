using Application.DTOs.ApplicationBlogDTO;
using Application.DTOs.ApplicationNewsAnnounceDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationCommon;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class NewsAnnounceService : INewsAnnounceService
    {
        private readonly INewsAnnounceRepository _newsAnnounceRepository;
        public NewsAnnounceService(INewsAnnounceRepository newsAnnounceRepository)
        {
            _newsAnnounceRepository = newsAnnounceRepository;
        }

        public async Task<NewsAnnounceDTO> CreateNewsAsync(CreateNewsAnnounceDTO createHomeContentDTO)
        {


            string savedImageName = string.Empty;
            if (createHomeContentDTO.File is { Length: > 0 })
            {
                savedImageName = await ImageHelper.SaveImageAsync(createHomeContentDTO.File, AppConstants.NewsContents);
                createHomeContentDTO.Image1 = savedImageName;

            }
            else
            {
                createHomeContentDTO.Image1 = null;
            }
            if (createHomeContentDTO.File2 is { Length: > 0 })
            {
                savedImageName = await ImageHelper.SaveImageAsync(createHomeContentDTO.File2, AppConstants.NewsContents);
                createHomeContentDTO.Image2 = savedImageName;
            }
            else
            {
                createHomeContentDTO.Image2 = null;
            }

            return await _newsAnnounceRepository.CreateNewsAsync(createHomeContentDTO);
        }

        public async Task<NewsAnnounceDTO> DeleteNewsAsync(long id)
        {
            return await _newsAnnounceRepository.DeleteNewsAsync(id);
        }

        public async Task<NewsAnnounceDTO> GetNewsByIdAsync(long id)
        {
            return await _newsAnnounceRepository.GetNewsByIdAsync(id);
        }

        public async Task<PaginatedList<NewsAnnounceDTO>> GetPagedNewsListAsync(int pageIndex, int pageSize, string title = null)
        {
            return await _newsAnnounceRepository.GetPagedNewsListAsync(pageIndex, pageSize, title);
        }

        public async Task<NewsAnnounceDTO> UpdateNewsAsync(UpdateNewsAnnounceDTO updateHomeContentDTO)
        {
            return await _newsAnnounceRepository.UpdateNewsAsync(updateHomeContentDTO);
        }
    }
}
