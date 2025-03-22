using FluentValidation;
using GHQ.Common.Exceptions;
using GHQ.Core.RollLogic.Handlers.Interfaces;
using GHQ.Core.RollLogic.Models;
using GHQ.Core.RollLogic.Queries;
using GHQ.Core.RollLogic.Requests;
using Microsoft.AspNetCore.Mvc;
using static GHQ.Core.RollLogic.Models.RollListVm;

namespace GHQ.API.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class RollController : Controller
{
    private readonly IRollHandler _rollHandler;
    private readonly ILogger<RollController> _logger;
    private readonly IValidator<AddRollRequest> _addValidator;
    private readonly IValidator<GetRollByIdQuery> _rollByIdValidator;
    private readonly IValidator<DeleteRollRequest> _deleteValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="RollController"/> class.
    /// </summary>
    /// <param name="rollHandler">A Roll object.</param>
    /// <param name="logger">A logger object for this controller.</param>
    /// <param name="rollByIdValidator">An IValidator object for get by id requests.</param>
    /// <param name="addValidator">An IValidator object for add requests.</param>
    /// <param name="deleteValidator">An IValidator object for delete requests.</param>
    public RollController(
        IRollHandler rollHandler,
        ILogger<RollController> logger,
        IValidator<GetRollByIdQuery> rollByIdValidator,
        IValidator<AddRollRequest> addValidator,
        IValidator<DeleteRollRequest> deleteValidator
        )
    {
        _rollHandler = rollHandler;
        _logger = logger;
        _rollByIdValidator = rollByIdValidator;
        _addValidator = addValidator;
        _deleteValidator = deleteValidator;
    }

    /// <summary>
    ///  Get rolls using optional filtering/sorting options.
    /// </summary>
    /// <param name="request">A request object inferred from the query in the URL.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet]
    public async Task<ActionResult<RollListVm>> GetAll(
        [FromQuery] GetRollListQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _rollHandler.GetAllRolls(request, cancellationToken);
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
    ///  Get Roll by Id.
    /// </summary>
    /// <param name="request">A request object inferred from the query in the URL.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet("{Id}")]
    public async Task<ActionResult<RollDto>> GetById(
        [FromRoute] GetRollByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _rollByIdValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _rollHandler.GetRollById(request, cancellationToken);
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
    ///  Add Roll.
    /// </summary>
    /// <param name="request">A request object inferred from the body.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPost("add")]
    public async Task<ActionResult<RollDto>> Add(
        [FromBody] AddRollRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _addValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _rollHandler.AddRoll(request, cancellationToken);
            return CreatedAtAction("Add", result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }

    /// <summary>
    ///  Delete Roll.
    /// </summary>
    /// <param name="request">A request object inferred from the query.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(
        [FromQuery] DeleteRollRequest request,
        CancellationToken cancellationToken = default)
    {
        var validateResult = await _deleteValidator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }
        try
        {
            await _rollHandler.DeleteRoll(request, cancellationToken);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }
}