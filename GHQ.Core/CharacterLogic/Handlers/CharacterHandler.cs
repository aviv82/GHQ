using AutoMapper;
using AutoMapper.QueryableExtensions;
using GHQ.Core.CharacterLogic.Handlers.Interfaces;
using GHQ.Core.CharacterLogic.Models;
using GHQ.Core.CharacterLogic.Queries;
using GHQ.Core.CharacterLogic.Requests;
using GHQ.Core.Extensions;
using GHQ.Data.Entities;
using GHQ.Data.EntityServices.Interfaces;
using static GHQ.Core.CharacterLogic.Models.CharacterListVm;

namespace GHQ.Core.CharacterLogic.Handlers;

public class CharacterHandler : ICharacterHandler
{
    private readonly IMapper _mapper;
    private readonly ICharacterService _characterService;
    private readonly IPlayerService _playerService;
    private readonly IGameService _gameService;

    public CharacterHandler(
        IMapper mapper,
        ICharacterService characterService,
        IPlayerService playerService,
        IGameService gameService)
    {
        _mapper = mapper;
        _characterService = characterService;
        _playerService = playerService;
        _gameService = gameService;
    }
    public async Task<CharacterListVm> GetAllCharacters(
     GetCharacterListQuery request,
     CancellationToken cancellationToken)
    {
        List<Character> query = await _characterService.GetAllAsync(cancellationToken);

        List<CharacterDto> characters = query
            .AsQueryable()
            .ApplyFiltering(request)
            .ApplySorting(request)
            .ApplyPaging(request)
            .ProjectTo<CharacterDto>(_mapper.ConfigurationProvider).ToList();

        return new CharacterListVm
        {
            CharacterList = characters,
            TotalCount = query.AsQueryable().Count(),
            CurrentPage = request.PageNumber,
            PageSize = request.PageSize
        };
    }


    public async Task<CharacterDto> GetCharacterById(
       GetCharacterByIdQuery request,
       CancellationToken cancellationToken)
    {
        Character? query = await _characterService.GetCharacterByIdIncludingPlayerAndGame(request.Id, cancellationToken);

        if (query == null) { throw new Exception("Character not found"); }

        List<Character> characterList = new List<Character> { query };

        var toReturn = characterList.AsQueryable().ProjectTo<CharacterDto>(_mapper.ConfigurationProvider);

        return toReturn.First();
    }

    public async Task<CharacterDto> AddCharacter(
    AddCharacterRequest request,
    CancellationToken cancellationToken)
    {
        try
        {

            Character characterToAdd = new Character
            {
                Name = request.Name,
                Image = request.Image ?? "",
                GameId = request.GameId,
                Game = new Game(),
                PlayerId = request.PlayerId,
                Player = new Player(),
            };

            Player player = await _playerService.GetByIdAsync(request.PlayerId, cancellationToken) ?? new Player();
            characterToAdd.Player = player;

            Game game = await _gameService.GetByIdAsync(request.GameId, cancellationToken) ?? new Game();
            characterToAdd.Game = game;


            Character newCharacter = await _characterService.InsertAsync(characterToAdd, cancellationToken);

            List<Character> chracterAsQueryable = new List<Character> { newCharacter };

            return chracterAsQueryable.AsQueryable().ProjectTo<CharacterDto>(_mapper.ConfigurationProvider).First();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public async Task<CharacterDto> UpdateCharacter(
     UpdateCharacterRequest request,
     CancellationToken cancellationToken)
    {
        try
        {
            var character = await _characterService.GetByIdAsync(request.Id, cancellationToken);

            if (character == null) { throw new Exception("Character not found"); };

            character.Name = request.Name;
            if (request.Image != null)
            {
                character.Image = request.Image;
            }

            await _characterService.UpdateAsync(character, cancellationToken);
            return _mapper.Map<CharacterDto>(character);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteCharacter(
    DeleteCharacterRequest request,
    CancellationToken cancellationToken)
    {
        try
        {
            var character = await _characterService.GetByIdAsync(request.Id, cancellationToken);

            if (character == null) { throw new Exception("Character not found"); };

            await _characterService.DeleteCascadeAsync(request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
