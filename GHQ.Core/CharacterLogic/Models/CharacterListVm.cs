using AutoMapper;
using GHQ.Common;
using GHQ.Core.DiceLogic.Models;
using GHQ.Core.Mappings;
using GHQ.Data.Entities;
using static GHQ.Core.GameLogic.Models.GameListVm;
using static GHQ.Core.PlayerLogic.Models.PlayerListVm;
using static GHQ.Core.RollLogic.Models.RollListVm;

namespace GHQ.Core.CharacterLogic.Models;

public class CharacterListVm : PaginationMetaData
{
    public ICollection<CharacterDto> CharacterList { get; set; } = default!;

    public class CharacterDto : IMapFrom<Character>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Image { get; set; }
        public int GameId { get; set; }
        public GameDto Game { get; set; } = new GameDto();
        public int PlayerId { get; set; }
        public PlayerDto Player { get; set; } = new PlayerDto();
        public List<RollDto> Rolls { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Character, CharacterDto>()
            .ForMember(dest => dest.Id
                , ops => ops.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name
                , ops => ops.MapFrom(src => src.Name))
            .ForMember(dest => dest.Image
                , ops => ops.MapFrom(src => src.Image))
            .ForMember(dest => dest.GameId
                , ops => ops.MapFrom(src => src.GameId))
            .ForMember(dest => dest.Game
                , ops => ops.MapFrom(src => MapGame(src.Game)))
            .ForMember(dest => dest.PlayerId
                , ops => ops.MapFrom(src => src.PlayerId))
            .ForMember(dest => dest.Player
                , ops => ops.MapFrom(src => MapPlayer(src.Player)))
            .ForMember(dest => dest.Rolls
                , ops => ops.MapFrom(src => MapRollList(src.Rolls)));
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
        public List<RollDto> MapRollList(ICollection<Roll> rollList)
        {
            List<RollDto> rollListToReturn = new List<RollDto>();

            if (rollList != null)
            {
                foreach (var roll in rollList)
                {
                    rollListToReturn.Add(
                    new RollDto
                    {
                        Id = roll.Id,
                        Title = roll.Title,
                        Description = roll.Description,
                        Difficulty = roll.Difficulty,
                        GameId = roll.GameId,
                        CharacterId = roll.CharacterId,
                        DicePool = roll.DicePool.ToList(),
                        Result = roll.Result.ToList()
                    });
                }
            }
            return rollListToReturn;
        }
        // public List<DiceDto> MapDiceList(ICollection<Dice> diceList)
        // {
        //     List<DiceDto> diceListToReturn = [];

        //     if (diceList != null)
        //     {
        //         foreach (var dice in diceList)
        //         {
        //             diceListToReturn.Add(
        //                 new DiceDto
        //                 {
        //                     Id = dice.Id,
        //                     Value = dice.Value,
        //                     Result = dice.Result
        //                 });
        //         }
        //     }
        //     return diceListToReturn;
        // }
    }
}
