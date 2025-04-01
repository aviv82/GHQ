using FluentValidation;
using GHQ.Core.TraitLogic.Handlers.Interfaces;
using GHQ.Core.TraitLogic.Models;
using GHQ.Core.TraitLogic.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GHQ.API.Controllers;
[Route("api/[controller]s")]
[ApiController]
public class TraitController : Controller
{
    private readonly ITraitHandler _traitHandler;
    private readonly ILogger<TraitController> _logger;
    private readonly IValidator<AddTraitRequest> _addValidator;
    private readonly IValidator<UpdateTraitRequest> _updateValidator;
    private readonly IValidator<DeleteTraitRequest> _deleteValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="TraitController"/> class.
    /// </summary>
    /// <param name="traitHandler">A Trait Group object.</param>
    /// <param name="logger">A logger object for this controller.</param>
    /// <param name="addValidator">An IValidator object for add requests.</param>
    /// <param name="updateValidator">An IValidator object for update requests.</param>
    /// <param name="deleteValidator">An IValidator object for delete requests.</param>
    public TraitController(
        ITraitHandler traitHandler,
        ILogger<TraitController> logger,
        IValidator<AddTraitRequest> addValidator,
        IValidator<UpdateTraitRequest> updateValidator,
        IValidator<DeleteTraitRequest> deleteValidator)
    {
        _traitHandler = traitHandler;
        _logger = logger;
        _addValidator = addValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    /// <summary>
    ///  Add Trait.
    /// </summary>
    /// <param name="request">A request object inferred from the body.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPost("add")]
    public async Task<ActionResult<TraitDto>> Add(
        [FromBody] AddTraitRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _addValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _traitHandler.AddTrait(request, cancellationToken);
            return CreatedAtAction("Add", result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }

    /// <summary>
    ///  Update Trait.
    /// </summary>
    /// <param name="request">A request object inferred from the body.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPut("edit")]
    public async Task<IActionResult> Update(
       [FromBody] UpdateTraitRequest request,
       CancellationToken cancellationToken = default)
    {
        var validateResult = await _updateValidator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }
        try
        {
            await _traitHandler.UpdateTrait(request, cancellationToken);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message);
        }
    }

    /// <summary>
    ///  Delete Trait.
    /// </summary>
    /// <param name="request">A request object inferred from the query.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(
        [FromQuery] DeleteTraitRequest request,
        CancellationToken cancellationToken = default)
    {
        var validateResult = await _deleteValidator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }
        try
        {
            await _traitHandler.DeleteTrait(request, cancellationToken);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }
}