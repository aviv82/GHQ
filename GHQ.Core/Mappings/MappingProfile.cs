using AutoMapper;
using GHQ.Core.Extensions;
using System.Reflection;

namespace GHQ.Core.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        var assembly = Assembly.GetExecutingAssembly();
        this.ApplyFromMappings(assembly);
        this.ApplyToMappings(assembly);
    }
}
