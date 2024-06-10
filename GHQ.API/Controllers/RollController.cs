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
    private readonly ILogger<GameController> _logger;
    private readonly IValidator<AddRollRequest> _addValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="RollController"/> class.
    /// </summary>
    /// <param name="rollHandler">A Character object.</param>
    /// <param name="logger">A logger object for this controller.</param>
    /// <param name="addValidator">An IValidator object for add requests.</param>
    public RollController(
        IRollHandler characterHandler,
        ILogger<GameController> logger,
        IValidator<AddRollRequest> addValidator
        )
    {
        _rollHandler = characterHandler;
        _logger = logger;
        _addValidator = addValidator;
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
}