using GHQ.Common;
using GHQ.Common.Interfaces;
using GHQ.Core.CharacterLogic.Models;
using GHQ.Core.Extensions;
using GHQ.Data.Entities;
using System.ComponentModel;

namespace GHQ.Core.CharacterLogic.Queries;

public class GetCharacterListQuery : QueryBase, IQueryWithSorting, IQueryWithPagination, IQueryWithFiltering
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
                        { } s when NameEquals(s, nameof(CharacterListVm.CharacterDto.Id))
                                => NameOf<Character>.Full(m => m.Id),
                        { } s when NameEquals(s, nameof(CharacterListVm.CharacterDto.Name))
                                => NameOf<Character>.Full(m => m.Name),
                        { } s when NameEquals(s, nameof(CharacterListVm.CharacterDto.Image))
                                => NameOf<Character>.Full(m => m.Image),
                        { } s when NameEquals(s, nameof(CharacterListVm.CharacterDto.GameId))
                                => NameOf<Character>.Full(m => m.GameId),
                        { } s when NameEquals(s, nameof(CharacterListVm.CharacterDto.Game))
                                => NameOf<Character>.Full(m => m.Game ?? new Game()),
                        { } s when NameEquals(s, nameof(CharacterListVm.CharacterDto.PlayerId))
                                => NameOf<Character>.Full(m => m.PlayerId),
                        { } s when NameEquals(s, nameof(CharacterListVm.CharacterDto.Player))
                                => NameOf<Character>.Full(m => m.Player ?? new Player()),
                        { } s when typeof(Character).GetProperties().Any(x => NameEquals(x.Name, name))
                                => name,
                        _ => string.Empty
                };
        }
}
