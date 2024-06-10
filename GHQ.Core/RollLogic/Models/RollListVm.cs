using AutoMapper;
using GHQ.Common;
using GHQ.Core.Mappings;
using GHQ.Data.Entities;
using static GHQ.Core.CharacterLogic.Models.CharacterListVm;
using static GHQ.Core.GameLogic.Models.GameListVm;
using static GHQ.Core.PlayerLogic.Models.PlayerListVm;

namespace GHQ.Core.RollLogic.Models;

public class RollListVm : PaginationMetaData
{
    public IEnumerable<RollDto> RollList { get; set; } = default!;

    public class RollDto : IMapFrom<Roll>
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string? Result { get; set; }
        public int GameId { get; set; }
        public GameDto Game { get; set; } = new GameDto();
        public int CharacterId { get; set; }
        public CharacterDto Character { get; set; } = new CharacterDto();

        // public List<DiceDto> DicePool {get; set;} =[];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Roll, RollDto>()
            .ForMember(dest => dest.Id
                , ops => ops.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title
                , ops => ops.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description
                , ops => ops.MapFrom(src => src.Description))
            .ForMember(dest => dest.GameId
                , ops => ops.MapFrom(src => src.GameId))
            .ForMember(dest => dest.Game
                , ops => ops.MapFrom(src => MapGame(src.Game)))
              .ForMember(dest => dest.CharacterId
                , ops => ops.MapFrom(src => src.CharacterId))
            .ForMember(dest => dest.Character
                , ops => ops.MapFrom(src => MapCharacter(src.Character)));
        }

        public CharacterDto MapCharacter(Character character)
        {
            CharacterDto characterToReturn = new CharacterDto();

            if (character != null)
            {
                {
                    characterToReturn.Id = character.Id;
                    characterToReturn.Name = character.Name;
                    characterToReturn.GameId = character.GameId;
                    characterToReturn.PlayerId = character.PlayerId;
                    characterToReturn.Image = character.Image ?? "";
                }
            }
            return characterToReturn;
        }

        public GameDto MapGame(Game game)
        {
            GameDto gameToReturn = new GameDto();

            if (game != null)
            {
                gameToReturn.Id = game.Id;
                gameToReturn.Title = game.Title;
                gameToReturn.Type = game.Type;
                gameToReturn.DmId = game.DmId;
                gameToReturn.Dm = MapPlayer(game.Dm);
            }
            return gameToReturn;
        }
        public PlayerDto MapPlayer(Player player)
        {
            PlayerDto playerToReturn = new PlayerDto();

            if (player != null)
            {
                playerToReturn.Id = player.Id;
                playerToReturn.UserName = player.UserName;
                playerToReturn.Email = player.Email ?? "";
            }
            return playerToReturn;
        }
    }
}