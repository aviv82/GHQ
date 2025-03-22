using GHQ.Core.CharacterLogic.Models;
using GHQ.Core.CharacterLogic.Queries;
using GHQ.Core.CharacterLogic.Requests;
using static GHQ.Core.CharacterLogic.Models.CharacterListVm;

namespace GHQ.Core.CharacterLogic.Handlers.Interfaces;

public interface ICharacterHandler
{
    Task<CharacterListVm> GetAllCharacters(GetCharacterListQuery request, CancellationToken cancellationToken);
    Task<CharacterDto> GetCharacterById(GetCharacterByIdQuery request, CancellationToken cancellationToken);
    Task<CharacterDto> GetCharacterSheetById(GetCharacterByIdQuery request, CancellationToken cancellationToken);
    Task<CharacterDto> AddCharacter(AddCharacterRequest request, CancellationToken cancellationToken);
    Task<CharacterDto> UpdateCharacter(UpdateCharacterRequest request, CancellationToken cancellationToken);
    Task DeleteCharacter(DeleteCharacterRequest request, CancellationToken cancellationToken);
}
