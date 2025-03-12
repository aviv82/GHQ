using AutoMapper;
using GHQ.Common;
using GHQ.Core.Mappings;
using GHQ.Data.Entities;
using static GHQ.Core.CharacterLogic.Models.CharacterListVm;
using static GHQ.Core.GameLogic.Models.GameListVm;

namespace GHQ.Core.PlayerLogic.Models;

public class PlayerListVm : PaginationMetaData
{
    public IEnumerable<PlayerDto> PlayerList { get; set; } = default!;

    public class PlayerDto : IMapFrom<Player>
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public string? Email { get; set; }
        public List<GameDto> PlayerGames { get; set; } = [];
        public List<GameDto> DmGames { get; set; } = [];
        public List<CharacterDto> Characters { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Player, PlayerDto>()
            .ForMember(dest => dest.Id
                , ops => ops.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName
                , ops => ops.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email
                , ops => ops.MapFrom(src => src.Email ?? string.Empty))
            .ForMember(dest => dest.PlayerGames
                , ops => ops.MapFrom(src => MapGames(src.PlayerGames)))
            .ForMember(dest => dest.DmGames
                , ops => ops.MapFrom(src => MapGames(src.DmGames)))
            .ForMember(dest => dest.Characters
                , ops => ops.MapFrom(src => MapCharacters(src.Characters)));
        }

        public List<CharacterDto> MapCharacters(ICollection<Character> characters)
        {
            List<CharacterDto> charactersToReturn = new List<CharacterDto>();

            if (characters != null)
            {
                characters.ToList().ForEach(x => charactersToReturn.Add(
                                new CharacterDto
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    GameId = x.GameId,
                                    PlayerId = x.PlayerId,
                                    Image = x.Image ?? string.Empty
                                }));
            }
            return charactersToReturn;
        }
        public List<GameDto> MapGames(ICollection<Game> games)
        {
            List<GameDto> gamesToReturn = new List<GameDto>();

            if (games != null)
            {
                games.ToList().ForEach(x => gamesToReturn.Add(
                                new GameDto
                                {
                                    Id = x.Id,
                                    DmId = x.DmId,
                                    Title = x.Title,
                                    Type = x.Type
                                }));
            }
            return gamesToReturn;
        }
    }
}

