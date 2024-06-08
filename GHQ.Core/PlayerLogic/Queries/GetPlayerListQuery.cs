using GHQ.Common;
using GHQ.Common.Interfaces;
using GHQ.Core.Extensions;
using GHQ.Core.PlayerLogic.Models;
using GHQ.Data.Entities;
using System.ComponentModel;

namespace GHQ.Core.PlayerLogic.Queries;

public class GetPlayerListQuery : QueryBase, IQueryWithSorting, IQueryWithPagination, IQueryWithFiltering
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
            { } s when NameEquals(s, nameof(PlayerListVm.PlayerDto.Id))
                    => NameOf<Player>.Full(m => m.Id),
            { } s when NameEquals(s, nameof(PlayerListVm.PlayerDto.Email))
                    => NameOf<Player>.Full(m => m.Email ?? ""),
            { } s when NameEquals(s, nameof(PlayerListVm.PlayerDto.UserName))
                    => NameOf<Player>.Full(m => m.UserName),
            { } s when NameEquals(s, nameof(PlayerListVm.PlayerDto.DmGames))
                    => NameOf<Player>.Full(m => m.DmGames),
            { } s when NameEquals(s, nameof(PlayerListVm.PlayerDto.PlayerGames))
                    => NameOf<Player>.Full(m => m.PlayerGames),
            { } s when typeof(Player).GetProperties().Any(x => NameEquals(x.Name, name))
                    => name,
            _ => string.Empty
        };
    }
}
