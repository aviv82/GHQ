using FluentValidation;
using GHQ.Common.Exceptions;
using GHQ.Core.CharacterLogic.Handlers.Interfaces;
using GHQ.Core.CharacterLogic.Models;
using GHQ.Core.CharacterLogic.Queries;
using GHQ.Core.CharacterLogic.Requests;
using Microsoft.AspNetCore.Mvc;
using static GHQ.Core.CharacterLogic.Models.CharacterListVm;

namespace GHQ.API.Controllers;

/// <summary>
/// CharacterController.
/// </summary>
[Route("api/[controller]s")]
[ApiController]
public class CharacterController : Controller
{
    private readonly ICharacterHandler _characterHandler;
    private readonly ILogger<GameController> _logger;
    IValidator<GetCharacterByIdQuery> _characterByIdValidator;
    IValidator<AddCharacterRequest> _addValidator;
    IValidator<UpdateCharacterRequest> _updateValidator;
    IValidator<DeleteCharacterRequest> _deleteValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterController"/> class.
    /// </summary>
    /// <param name="characterHandler">A Character object.</param>
    /// <param name="logger">A logger object for this controller.</param>
    /// <param name="characterByIdValidator">An IValidator object for get by id requests.</param>
    /// <param name="addValidator">An IValidator object for add requests.</param>
    /// <param name="updateValidator">An IValidator object for update requests.</param>
    /// <param name="deleteValidator">An IValidator object for delete requests.</param>
    public CharacterController(
        ICharacterHandler characterHandler,
        ILogger<GameController> logger,
        IValidator<GetCharacterByIdQuery> characterByIdValidator,
        IValidator<AddCharacterRequest> addValidator,
        IValidator<UpdateCharacterRequest> updateValidator,
         IValidator<DeleteCharacterRequest> deleteValidator
        )
    {
        _characterHandler = characterHandler;
        _logger = logger;
        _characterByIdValidator = characterByIdValidator;
        _addValidator = addValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    /// <summary>
    ///  Get Characters using optional filtering/sorting options.
    /// </summary>
    /// <param name="request">A request object inferred from the query in the URL.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet]
    public async Task<ActionResult<CharacterListVm>> GetAll(
        [FromQuery] GetCharacterListQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _characterHandler.GetAllCharacters(request, cancellationToken);
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
    ///  Get Character by its Id.
    /// </summary>
    /// <param name="request">A request object inferred from the query in the URL.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet("{Id}")]
    public async Task<ActionResult<CharacterDto>> GetById(
        [FromRoute] GetCharacterByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _characterByIdValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _characterHandler.GetCharacterById(request, cancellationToken);
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
    ///  Add Character.
    /// </summary>
    /// <param name="request">A request object inferred from the body.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPost("add")]
    public async Task<ActionResult<CharacterDto>> Add(
        [FromBody] AddCharacterRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _addValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _characterHandler.AddCharacter(request, cancellationToken);
            return CreatedAtAction("Add", result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }

    /// <summary>
    ///  Update Character.
    /// </summary>
    /// <param name="request">A request object inferred from the body.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPut("edit")]
    public async Task<IActionResult> Update(
       [FromBody] UpdateCharacterRequest request,
       CancellationToken cancellationToken = default)
    {
        var validateResult = await _updateValidator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }
        try
        {
            await _characterHandler.UpdateCharacter(request, cancellationToken);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message);
        }
    }

    /// <summary>
    ///  Delete Character.
    /// </summary>
    /// <param name="request">A request object inferred from the query.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(
        [FromQuery] DeleteCharacterRequest request,
        CancellationToken cancellationToken = default)
    {
        var validateResult = await _deleteValidator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }

        try
        {
            await _characterHandler.DeleteCharacter(request, cancellationToken);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }
}
