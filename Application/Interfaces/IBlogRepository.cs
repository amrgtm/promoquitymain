using Application.DTOs.ApplicationBlogDTO;
using Application.Helpers;

namespace Application.Interfaces
{
    public interface IBlogRepository
    {
        Task<BlogDTO> CreateBlogAsync(CreateBlogDTO createBlogDTO);
        Task<BlogDTO> UpdateBlogAsync(UpdateBlogDTO updateBlogDTO);
        Task<BlogDTO> DeleteBlogAsync(long id);
        Task<BlogDTO> GetBlogByIdAsync(long id);
        Task<PaginatedList<BlogDTO>> GetPagedBlogListAsync(int pageIndex, int pageSize, string blogId = null, string blogTitle = null);
    }
}
