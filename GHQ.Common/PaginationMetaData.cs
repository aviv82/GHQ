namespace GHQ.Common;

public abstract class PaginationMetaData
{
    public int? CurrentPage { get; set; }
    public int? TotalPages => PageSize.HasValue ? (int)Math.Ceiling(TotalCount / (double)PageSize.Value) : (int?)null;
    public int? PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious => CurrentPage.HasValue && CurrentPage > 1;
    public bool HasNext => CurrentPage.HasValue && CurrentPage < TotalPages;
}
