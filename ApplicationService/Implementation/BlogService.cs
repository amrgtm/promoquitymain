using Application.DTOs.ApplicationBlogDTO;
using Application.DTOs.ApplicationHomeContentDTO;
using Application.Helpers;
using Application.Interfaces;
using ApplicationCommon;
using ApplicationService.Interface;

namespace ApplicationService.Implementation
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogDTO> CreateBlogAsync(CreateBlogDTO createBlogDTO)
        {
           // return await _blogRepository.CreateBlogAsync(createBlogDTO);



            string savedImageName = string.Empty;
            if (createBlogDTO.File is { Length: > 0 })
            {
                savedImageName = await ImageHelper.SaveImageAsync(createBlogDTO.File, AppConstants.HomeContents);
                createBlogDTO.Image1 = savedImageName;
            }
            else
            {
                createBlogDTO.Image1 = null;
            }
            if (createBlogDTO.File2 is { Length: > 0 })
            {
                savedImageName = await ImageHelper.SaveImageAsync(createBlogDTO.File2, AppConstants.HomeContents);
                createBlogDTO.Image2 = savedImageName;
            }
            else
            {
                createBlogDTO.Image2 = null;
            }
                return await _blogRepository.CreateBlogAsync(createBlogDTO);
        }

        public async Task<BlogDTO> DeleteBlogAsync(long id)
        {
            return await _blogRepository.DeleteBlogAsync(id);
        }

        public async Task<BlogDTO> GetBlogByIdAsync(long id)
        {
            return await _blogRepository.GetBlogByIdAsync(id);
        }

        public async Task<PaginatedList<BlogDTO>> GetPagedBlogListAsync(int pageIndex, int pageSize, string blogId = null, string blogTitle = null)
        {
            return await _blogRepository.GetPagedBlogListAsync(pageIndex, pageSize, blogId, blogTitle);
        }

        public async Task<BlogDTO> UpdateBlogAsync(UpdateBlogDTO updateBlogDTO)
        {
            return await _blogRepository.UpdateBlogAsync(updateBlogDTO);
        }
    }
}
