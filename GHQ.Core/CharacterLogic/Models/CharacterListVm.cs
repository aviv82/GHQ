using AutoMapper;
using GHQ.Common;
using GHQ.Core.Mappings;
using GHQ.Core.TraitGroupLogic.Models;
using GHQ.Core.TraitLogic.Models;
using GHQ.Data.Entities;
using static GHQ.Core.GameLogic.Models.GameListVm;
using static GHQ.Core.PlayerLogic.Models.PlayerListVm;

namespace GHQ.Core.CharacterLogic.Models;

public class CharacterListVm : PaginationMetaData
{
    public ICollection<CharacterDto> CharacterList { get; set; } = default!;

    public class CharacterDto : IMapFrom<Character>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Image { get; set; }
        public int? GameId { get; set; }
        public GameDto? Game { get; set; }
        public int? PlayerId { get; set; }
        public PlayerDto? Player { get; set; }
        public List<TraitGroupDto>? TraitGroups { get; set; }

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
            .ForMember(dest => dest.TraitGroups
                , ops => ops.MapFrom(src => MapTraitGroupList(src.TraitGroups)));
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

        public List<TraitGroupDto>? MapTraitGroupList(ICollection<TraitGroup> traitGroupList)
        {
            if (traitGroupList == null) return null;

            List<TraitGroupDto> traitGroupsToReturn = [];

            foreach (var traitGroup in traitGroupList)
            {
                traitGroupsToReturn.Add(MapTraitGroup(traitGroup));
            }

            return traitGroupsToReturn;
        }

        public TraitGroupDto MapTraitGroup(TraitGroup traitGroup)
        {
            List<TraitDto> traits = [];

            foreach (var trait in traitGroup.Traits)
            {
                traits.Add(MapTrait(trait));
            }

            return new TraitGroupDto
            {
                Id = traitGroup.Id,
                TraitGroupName = traitGroup.TraitGroupName,
                Type = traitGroup.Type,
                CharacterId = traitGroup.CharacterId,
                Traits = traits
            };
        }
        public TraitDto MapTrait(Trait trait)
        {
            return new TraitDto
            {
                Id = trait.Id,
                Details = trait.Details,
                Name = trait.Name,
                Level = trait.Level,
                Value = trait.Value,
                TraitGroupId = trait.TraitGroupId ?? 0
            };
        }
    }
}
