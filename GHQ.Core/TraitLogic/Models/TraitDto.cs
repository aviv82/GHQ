using AutoMapper;
using GHQ.Core.Mappings;
using GHQ.Data.Entities;

namespace GHQ.Core.TraitLogic.Models;
public class TraitDto : IMapFrom<Trait>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int? Value { get; set; }
    public int? Level { get; set; }
    public int TraitGroupId { get; set; }
    // public TraitGroup TraitGroup { get; set; } = default!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Trait, TraitDto>()
        .ForMember(dest => dest.Id
            , ops => ops.MapFrom(src => src.Id))
        .ForMember(dest => dest.Name
            , ops => ops.MapFrom(src => src.Name))
        .ForMember(dest => dest.Value
            , ops => ops.MapFrom(src => src.Value))
        .ForMember(dest => dest.Level
            , ops => ops.MapFrom(src => src.Level))
        .ForMember(dest => dest.TraitGroupId
            , ops => ops.MapFrom(src => src.TraitGroupId));
    }
}