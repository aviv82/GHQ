using FluentValidation;
using GHQ.Common.Exceptions;
using GHQ.Core.PlayerLogic.Handlers.Interfaces;
using GHQ.Core.PlayerLogic.Models;
using GHQ.Core.PlayerLogic.Queries;
using GHQ.Core.PlayerLogic.Requests;
using Microsoft.AspNetCore.Mvc;
using static GHQ.Core.PlayerLogic.Models.PlayerListVm;

namespace GHQ.API.Controllers;

/// <summary>
/// PlayerController.
/// </summary>
[Route("api/[controller]s")]
[ApiController]
public class PlayerController : Controller
{
    private readonly IPlayerHandler _playerHandler;
    private readonly IValidator<GetPlayerByIdQuery> _playerByIdValidator;
    private readonly IValidator<AddPlayerRequest> _addValidator;
    private readonly IValidator<UpdatePlayerRequest> _updateValidator;
    private readonly IValidator<DeletePlayerRequest> _deleteValidator;


    private readonly ILogger<PlayerController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerController"/> class.
    /// </summary>
    /// <param name="playerHandler">A Player object.</param>
    /// <param name="playerByIdValidator">An IValidator object for fetching object by id requests.</param>
    /// <param name="addValidator">An IValidator object for add requests.</param>
    /// <param name="updateValidator">An IValidator object for update requests.</param>
    /// <param name="deleteValidator">An IValidator object for delete requests.</param>
    /// <param name="logger">A logger object for this controller.</param>
    public PlayerController(
        IPlayerHandler playerHandler,
        IValidator<GetPlayerByIdQuery> playerByIdValidator,
        IValidator<AddPlayerRequest> addValidator,
        IValidator<UpdatePlayerRequest> updateValidator,
        IValidator<DeletePlayerRequest> deleteValidator,


        ILogger<PlayerController> logger)
    {
        _addValidator = addValidator;
        _updateValidator = updateValidator;
        _playerByIdValidator = playerByIdValidator;
        _deleteValidator = deleteValidator;

        _playerHandler = playerHandler;
        _logger = logger;
    }

    /// <summary>
    ///  Get Players using optional filtering/sorting options.
    /// </summary>
    /// <param name="request">A request object inferred from the query in the URL.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet]
    public async Task<ActionResult<PlayerListVm>> GetAll(
        [FromQuery] GetPlayerListQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _playerHandler.GetAllPlayers(request, cancellationToken);
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
    ///  Get Player by its Id
    /// </summary>
    /// <param name="request">A request object inferred from the query in the URL.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet("{Id}")]
    public async Task<ActionResult<PlayerDto>> GetById(
        [FromRoute] GetPlayerByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _playerByIdValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _playerHandler.GetPlayerById(request, cancellationToken);
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
    ///  Add Player.
    /// </summary>
    /// <param name="request">A request object inferred from the body.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPost("add")]
    public async Task<ActionResult<PlayerDto>> Add(
        [FromBody] AddPlayerRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _addValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _playerHandler.AddPlayer(request, cancellationToken);
            return CreatedAtAction("Add", result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }

    /// <summary>
    ///  Update Player.
    /// </summary>
    /// <param name="request">A request object inferred from the body.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPut("edit")]
    public async Task<IActionResult> Update(
       [FromBody] UpdatePlayerRequest request,
       CancellationToken cancellationToken = default)
    {
        var validateResult = await _updateValidator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }
        try
        {
            await _playerHandler.UpdatePlayer(request, cancellationToken);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message);
        }
    }

    /// <summary>
    ///  Delete Player.
    /// </summary>
    /// <param name="request">A request object inferred from the query.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(
        [FromQuery] DeletePlayerRequest request,
        CancellationToken cancellationToken = default)
    {
        var validateResult = await _deleteValidator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }

        try
        {
            await _playerHandler.DeletePlayer(request, cancellationToken);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }
}
