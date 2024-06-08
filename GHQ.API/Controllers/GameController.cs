using FluentValidation;
using GHQ.Common.Exceptions;
using GHQ.Core.GameLogic.Handlers.Interfaces;
using GHQ.Core.GameLogic.Models;
using GHQ.Core.GameLogic.Queries;
using GHQ.Core.GameLogic.Requests;
using Microsoft.AspNetCore.Mvc;
using static GHQ.Core.GameLogic.Models.GameListVm;

namespace GHQ.API.Controllers;


/// <summary>
/// GameController.
/// </summary>
[Route("api/[controller]s")]
[ApiController]
public class GameController : Controller
{
    private readonly IGameHandler _gameHandler;
    private readonly ILogger<GameController> _logger;

    IValidator<GetGameByIdQuery> _gameByIdValidator;
    IValidator<AddGameRequest> _addValidator;
    IValidator<UpdateGameRequest> _updateValidator;
    IValidator<DeleteGameRequest> _deleteValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameController"/> class.
    /// </summary>
    /// <param name="gameHandler">A Player object.</param>
    /// <param name="logger">A logger object for this controller.</param>
    /// <param name="gameByIdValidator">An IValidator object for fetching object by id requests.</param>
    /// <param name="addValidator">An IValidator object for add requests.</param>
    /// <param name="updateValidator">An IValidator object for update requests.</param>
    /// <param name="deleteValidator">An IValidator object for delet requests.</param>
    public GameController(
        IGameHandler gameHandler,
        ILogger<GameController> logger,
        IValidator<GetGameByIdQuery> gameByIdValidator,
        IValidator<AddGameRequest> addValidator,
        IValidator<UpdateGameRequest> updateValidator,
        IValidator<DeleteGameRequest> deleteValidator
        )
    {
        _gameByIdValidator = gameByIdValidator;
        _addValidator = addValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;

        _gameHandler = gameHandler;
        _logger = logger;
    }

    /// <summary>
    ///  Get Games using optional filtering/sorting options.
    /// </summary>
    /// <param name="request">A request object inferred from the query in the URL.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet]
    public async Task<ActionResult<GameListVm>> GetAll(
        [FromQuery] GetGameListQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _gameHandler.GetAllGames(request, cancellationToken);
            return Ok(result);
        }
        catch (BusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }

    /// <summary>
    ///  Get Game by its Id.
    /// </summary>
    /// <param name="request">A request object inferred from the query in the URL.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet("{Id}")]
    public async Task<ActionResult<GameDto>> GetById(
        [FromRoute] GetGameByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _gameByIdValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _gameHandler.GetGameById(request, cancellationToken);
            return Ok(result);
        }
        catch (BusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }

    /// <summary>
    ///  Add Game.
    /// </summary>
    /// <param name="request">A request object inferred from the body.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPost("add")]
    public async Task<ActionResult<GameDto>> Add(
        [FromBody] AddGameRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _addValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _gameHandler.AddGame(request, cancellationToken);
            return CreatedAtAction("Add", result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }


    /// <summary>
    ///  Update Game.
    /// </summary>
    /// <param name="request">A request object inferred from the body.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPut("edit")]
    public async Task<IActionResult> Update(
       [FromBody] UpdateGameRequest request,
       CancellationToken cancellationToken = default)
    {
        var validateResult = await _updateValidator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }
        try
        {
            await _gameHandler.UpdateGame(request, cancellationToken);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message);
        }
    }

    /// <summary>
    ///  Delete Game.
    /// </summary>
    /// <param name="request">A request object inferred from the query.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(
        [FromQuery] DeleteGameRequest request,
        CancellationToken cancellationToken = default)
    {
        var validateResult = await _deleteValidator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }

        try
        {
            await _gameHandler.DeleteGame(request, cancellationToken);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }
}
