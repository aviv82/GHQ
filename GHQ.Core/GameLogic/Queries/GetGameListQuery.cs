using GHQ.Common.Interfaces;
using GHQ.Core.Extensions;
using GHQ.Data.Entities;
using System.ComponentModel;
using GHQ.Common;
using GHQ.Core.GameLogic.Models;

namespace GHQ.Core.GameLogic.Queries;

public class GetGameListQuery : QueryBase, IQueryWithSorting, IQueryWithPagination, IQueryWithFiltering
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
            { } s when NameEquals(s, nameof(GameListVm.GameDto.Id))
                    => NameOf<Game>.Full(m => m.Id),
            { } s when NameEquals(s, nameof(GameListVm.GameDto.Title))
                    => NameOf<Game>.Full(m => m.Title),
            { } s when NameEquals(s, nameof(GameListVm.GameDto.Type))
                    => NameOf<Game>.Full(m => m.Type),
            { } s when NameEquals(s, nameof(GameListVm.GameDto.DmId))
                    => NameOf<Game>.Full(m => m.DmId),
            { } s when NameEquals(s, nameof(GameListVm.GameDto.Dm))
                    => NameOf<Game>.Full(m => m.Dm),
            { } s when NameEquals(s, nameof(GameListVm.GameDto.Players))
                    => NameOf<Game>.Full(m => m.Players),
            { } s when typeof(Game).GetProperties().Any(x => NameEquals(x.Name, name))
                    => name,
            _ => string.Empty
        };
    }
}


