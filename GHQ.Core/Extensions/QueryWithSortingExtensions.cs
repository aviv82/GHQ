using GHQ.Common.Interfaces;

namespace GHQ.Core.Extensions;

public static class QueryWithSortingExtensions
{
    public static bool IsSortDescending(this IQueryWithSorting query)
    {
        return !string.IsNullOrEmpty(query.Sort) && query.Sort.StartsWith('-');
    }

    public static string GetColumnNameForSort<T>(this IQueryWithSorting queryWithSorting)
    {
        var sortString = queryWithSorting.IsSortDescending() ? queryWithSorting.Sort.Remove(0, 1) : queryWithSorting.Sort;
        return queryWithSorting.GetColumnName<T>(sortString);
    }
}
