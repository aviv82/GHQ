using AutoMapper;
using GHQ.Common;
using GHQ.Common.Enums;
using GHQ.Core.Mappings;
using GHQ.Data.Entities;
using static GHQ.Core.CharacterLogic.Models.CharacterListVm;
using static GHQ.Core.GameLogic.Models.GameListVm;
using static GHQ.Core.PlayerLogic.Models.PlayerListVm;

namespace GHQ.Core.RollLogic.Models;

public class RollListVm : PaginationMetaData
{
    public ICollection<RollDto> RollList { get; set; } = default!;

    public class RollDto : IMapFrom<Roll>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Difficulty { get; set; }
        public int GameId { get; set; }
        public GameDto Game { get; set; } = default!;
        public int? PlayerId { get; set; }
        public PlayerDto? Player { get; set; }
        public int? CharacterId { get; set; }
        public CharacterDto? Character { get; set; }
        public List<int> DicePool { get; set; } = [];
        public List<int> Result { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Roll, RollDto>()
            .ForMember(dest => dest.Id
                , ops => ops.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title
                , ops => ops.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description
                , ops => ops.MapFrom(src => src.Description))
            .ForMember(dest => dest.Difficulty
                , ops => ops.MapFrom(src => src.Difficulty))
            .ForMember(dest => dest.GameId
                , ops => ops.MapFrom(src => src.GameId))
            .ForMember(dest => dest.Game
                , ops => ops.MapFrom(src => MapGame(src.Game)))
            .ForMember(dest => dest.PlayerId
                , ops => ops.MapFrom(src => src.PlayerId))
            .ForMember(dest => dest.Player
                , ops => ops.MapFrom(src => MapPlayer(src.Player)))
            .ForMember(dest => dest.CharacterId
                , ops => ops.MapFrom(src => src.CharacterId))
            .ForMember(dest => dest.Character
                , ops => ops.MapFrom(src => MapCharacter(src.Character)))
            .ForMember(dest => dest.DicePool
                , ops => ops.MapFrom(src => src.DicePool.ToList()))
            .ForMember(dest => dest.Result
                , ops => ops.MapFrom(src => src.Result.ToList()));
        }

        public CharacterDto? MapCharacter(Character character)
        {
            if (character == null) return null;
            return new CharacterDto
            {
                Id = character.Id,
                Name = character.Name,
                GameId = character.GameId,
                PlayerId = character.PlayerId,
                Image = character.Image
            };
        }

        public GameDto? MapGame(Game game)
        {
            if (game == null) return null;
            return new GameDto
            {
                Id = game.Id,
                Title = game.Title,
                Type = game.Type,
                DmId = game.DmId,
            };
        }
        public PlayerDto? MapPlayer(Player player)
        {
            if (player == null) return null;
            return new PlayerDto
            {
                Id = player.Id,
                UserName = player.UserName,
                Email = player.Email
            };
        }
    }
}
