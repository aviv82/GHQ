using AutoMapper;

namespace GHQ.Core.Mappings;

public interface IMapTo<T>
{
    void Mapping(Profile profile);
}
