using Application.Helpers;

namespace Application.Interfaces
{
    public interface IGenericRepository<TEntity, TCreateDto, TUpdateDto, TListDto, TSearchDto>
   where TEntity : class
    {
        Task<IEnumerable<TListDto>> GetAllAsync();
        Task<TListDto> GetByIdAsync(long id);
        Task<long> AddAsync(TCreateDto dto);
        Task UpdateAsync(TUpdateDto dto);
        Task DeleteAsync(long id);
        Task<PaginatedList<TListDto>> GetPagedAsync(int pageIndex, int pageSize);
        Task<PaginatedList<TListDto>> GetPagedAsync(PagedRequestDto pagedRequest);

        Task<PaginatedList<TListDto>> GetWithJoinsAsync<TJoinEntity>(Func<IQueryable<TEntity>, IQueryable<TJoinEntity>> joinQuery, int pageIndex, int pageSize, List<FilterCriteriaDto>? filters = null);
    }
}
