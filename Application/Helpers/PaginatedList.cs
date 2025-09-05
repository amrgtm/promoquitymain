namespace Application.Helpers
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; private set; }
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }
    }
    public class PagedRequestDto
    {
        public int PageIndex { get; set; } = 1;  // Default to the first page
        public int PageSize { get; set; } = 10;  // Default page size

        public string SearchTerm { get; set; }   
        public List<FilterCriteriaDto> Filters { get; set; } = new List<FilterCriteriaDto>();
    }
    public class FilterCriteriaDto
    {
        public string FieldName { get; set; }  // The property name to filter on
        public string Operator { get; set; }   // Comparison operator, e.g., '=', '>', '<', 'Contains'
        public string Value { get; set; }      // The value to filter by
    }
}
