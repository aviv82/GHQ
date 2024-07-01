using AutoMapper;
using GHQ.Common;
using GHQ.Common.Enums;
using GHQ.Core.DiceLogic.Models;
using GHQ.Core.Mappings;
using GHQ.Data.Entities;
using static GHQ.Core.CharacterLogic.Models.CharacterListVm;
using static GHQ.Core.PlayerLogic.Models.PlayerListVm;
using static GHQ.Core.RollLogic.Models.RollListVm;

namespace GHQ.Core.GameLogic.Models;

public class GameListVm : PaginationMetaData
{
    public ICollection<GameDto> GameList { get; set; } = default!;

    public class GameDto : IMapFrom<Game>
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public GameType Type { get; set; }
        public int DmId { get; set; }
        public PlayerDto Dm { get; set; } = new PlayerDto();
        public List<PlayerDto> Players { get; set; } = [];
        public List<CharacterDto> Characters { get; set; } = [];
        public List<RollDto> Rolls { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Game, GameDto>()
            .ForMember(dest => dest.Id
                , ops => ops.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title
                , ops => ops.MapFrom(src => src.Title))
            .ForMember(dest => dest.Type
                , ops => ops.MapFrom(src => src.Type))
            .ForMember(dest => dest.DmId
                , ops => ops.MapFrom(src => src.DmId))
            .ForMember(dest => dest.Dm
                , ops => ops.MapFrom(src => MapPlayer(src.Dm)))
            .ForMember(dest => dest.Players
                , ops => ops.MapFrom(src => MapPlayerList(src.Players)))
            .ForMember(dest => dest.Characters
                , ops => ops.MapFrom(src => MapCharacterList(src.Characters)))
            .ForMember(dest => dest.Rolls
                , ops => ops.MapFrom(src => MapRollList(src.Rolls)));
        }

        public List<CharacterDto> MapCharacterList(ICollection<Character> list)
        {
            List<CharacterDto> characterListToReturn = new List<CharacterDto>();

            if (list != null)
            {
                foreach (var character in list) { characterListToReturn.Add(MapCharacter(character)); };
            }

            return characterListToReturn;
        }

        public CharacterDto MapCharacter(Character character)
        {
            CharacterDto characterToReturn = new CharacterDto();

            if (character != null)
            {
                characterToReturn.Id = character.Id;
                characterToReturn.Name = character.Name;
                characterToReturn.GameId = character.GameId;
                characterToReturn.PlayerId = character.PlayerId;
                characterToReturn.Image = character.Image ?? "";
            }
            return characterToReturn;
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
                        DicePool = MapDicePool(roll.DicePool),
                        Result = roll.Result.ToList()
                    });
                }
            }
            return rollListToReturn;
        }

        public List<DiceType> MapDicePool(List<DiceType> dicePool)
        {
            List<DiceType> toReturn = [];
            if (dicePool != null)
            {
                foreach (var dice in dicePool)
                {
                    toReturn.Add(dice);
                }
            }
            return toReturn;
        }

        public List<PlayerDto> MapPlayerList(ICollection<Player> list)
        {
            List<PlayerDto> playerListToReturn = new List<PlayerDto>();

            if (list != null)
            {
                foreach (var player in list) { playerListToReturn.Add(MapPlayer(player)); };
            }

            return playerListToReturn;
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

