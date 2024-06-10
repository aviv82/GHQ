
using System.ComponentModel;
using GHQ.Common;
using GHQ.Common.Interfaces;
using GHQ.Core.Extensions;
using GHQ.Core.RollLogic.Models;
using GHQ.Data.Entities;

namespace GHQ.Core.RollLogic.Queries;

public class GetRollListQuery : QueryBase, IQueryWithSorting, IQueryWithPagination, IQueryWithFiltering
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string Filter { get; set; } = string.Empty;
    public string Sort { get; set; } = string.Empty;

    [DefaultValue(Constants.Filtering.DefaultKeyValueSeparator)]
    public char FilterKeyValueSeparator { get; set; } = Constants.Filtering.DefaultKeyValueSeparator;

    [DefaultValue(Constants.Filtering.DefaultItemSeparator)]
    public char FilterItemSeparator { get; set; } = Constants.Filtering.DefaultItemSeparator;

    public override string GetColumnName<T>(string name)
    {
        return name switch
        {
            { } s when NameEquals(s, nameof(RollListVm.RollDto.Id))
                    => NameOf<Roll>.Full(m => m.Id),
            { } s when NameEquals(s, nameof(RollListVm.RollDto.Title))
                    => NameOf<Roll>.Full(m => m.Title),
            { } s when NameEquals(s, nameof(RollListVm.RollDto.Description))
                    => NameOf<Roll>.Full(m => m.Description ?? ""),
            { } s when NameEquals(s, nameof(RollListVm.RollDto.Result))
                    => NameOf<Roll>.Full(m => m.Result ?? ""),
            { } s when NameEquals(s, nameof(RollListVm.RollDto.GameId))
                    => NameOf<Roll>.Full(m => m.GameId),
            { } s when NameEquals(s, nameof(RollListVm.RollDto.Game))
                    => NameOf<Roll>.Full(m => m.Game),
            { } s when NameEquals(s, nameof(RollListVm.RollDto.CharacterId))
                    => NameOf<Roll>.Full(m => m.CharacterId),
            { } s when NameEquals(s, nameof(RollListVm.RollDto.Character))
                    => NameOf<Roll>.Full(m => m.Character),
            { } s when typeof(Roll).GetProperties().Any(x => NameEquals(x.Name, name))
                    => name,
            _ => string.Empty
        };
    }
}

