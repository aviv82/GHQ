using GHQ.Data.Entities;

namespace GHQ.Data.EntityServices.Interfaces;

public interface ICharacterService : IBaseService<Character>
{
    Task<Character> GetCharacterByIdIncludingPlayerAndGame(int id, CancellationToken cancellationToken);
}
