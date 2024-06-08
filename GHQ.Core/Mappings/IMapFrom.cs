using AutoMapper;

namespace GHQ.Core.Mappings;

public interface IMapFrom<T>
{
    void Mapping(Profile profile);
}
