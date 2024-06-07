using GHQ.Common.Interfaces;

namespace GHQ.Core.Extensions;

public static class QueryWithFilteringExtensions
{
    public static Dictionary<string, string> GetFilterDictionary<T>(this IQueryWithFiltering queryWithFiltering)
    {
        var filter = new Dictionary<string, string>();
        if (string.IsNullOrEmpty(queryWithFiltering.Filter)) return filter;
        var filterList = queryWithFiltering.Filter.Split(queryWithFiltering.FilterItemSeparator);
        foreach (var filterItem in filterList)
        {
            var parts = filterItem.Split(queryWithFiltering.FilterKeyValueSeparator);
            if (parts.Length != 2) continue;
            var columnName = queryWithFiltering.GetColumnName<T>(parts[0]);
            if (!string.IsNullOrEmpty(columnName))
            {
                filter.Add(columnName, parts[1].Trim());
            }
        }

        return filter;
    }
}
