using AutoMapper;
using GHQ.Core.Mappings;
using GHQ.Core.TraitLogic.Models;
using GHQ.Data.Entities;
using GHQ.Data.Enums;

namespace GHQ.Core.TraitGroupLogic.Models;

public class TraitGroupDto : IMapFrom<TraitGroup>
{
    public int Id { get; set; }
    public string TraitGroupName { get; set; } = default!;
    public TraitType? Type { get; set; }
    public int? CharacterId { get; set; }
    public List<TraitDto>? Traits { get; set; } = [];

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TraitGroup, TraitGroupDto>()
        .ForMember(dest => dest.Id
            , ops => ops.MapFrom(src => src.Id))
        .ForMember(dest => dest.TraitGroupName
            , ops => ops.MapFrom(src => src.TraitGroupName))
        .ForMember(dest => dest.Type
            , ops => ops.MapFrom(src => src.Type))
        .ForMember(dest => dest.Traits
            , ops => ops.MapFrom(src => TraitListMapper(src.Traits)))
        .ForMember(dest => dest.CharacterId
            , ops => ops.MapFrom(src => src.CharacterId));
    }

    public List<TraitDto> TraitListMapper(ICollection<Trait> traitList)
    {
        List<TraitDto> listToReturn = [];

        if (traitList != null)
        {
            foreach (Trait trait in traitList)
            {
                listToReturn.Add(new TraitDto
                {
                    Id = trait.Id,
                    Name = trait.Name,
                    Details = trait.Details ?? null,
                    Value = trait.Value ?? null,
                    Level = trait.Level ?? null,
                    TraitGroupId = trait.TraitGroupId ?? 0
                });
            }
        }
        return listToReturn;
    }

    // public CharacterDto MapCharacter(Character character)
    // {
    //     CharacterDto characterToReturn = new CharacterDto();

    //     if (character != null)
    //     {
    //         {
    //             characterToReturn.Id = character.Id;
    //             characterToReturn.Name = character.Name;
    //             characterToReturn.GameId = character.GameId;
    //             characterToReturn.PlayerId = character.PlayerId;
    //             characterToReturn.Image = character.Image ?? "";
    //         }
    //     }
    //     return characterToReturn;
    // }
}
