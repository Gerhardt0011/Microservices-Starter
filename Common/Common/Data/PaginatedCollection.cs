namespace Common.Data;

public class PaginatedCollection<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public long TotalPages { get; set; }
    public long TotalCount { get; set; }
    public IEnumerable<T> Items { get; set; } = null!;

    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    public PaginatedCollection(IEnumerable<T> items, long count, int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = count;
        TotalPages = (long)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }
}