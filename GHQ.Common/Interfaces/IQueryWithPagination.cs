namespace GHQ.Common.Interfaces;

public interface IQueryWithPagination : IQueryBase
{
    public int? PageNumber { get; set; }

    public int? PageSize { get; set; }
}
