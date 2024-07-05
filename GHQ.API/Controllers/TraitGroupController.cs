using FluentValidation;
using GHQ.Common.Exceptions;
using GHQ.Core.TraitGroupLogic.Handlers.Interfaces;
using GHQ.Core.TraitGroupLogic.Models;
using GHQ.Core.TraitGroupLogic.Queries;
using GHQ.Core.TraitGroupLogic.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GHQ.API.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class TraitGroupController : Controller
{
    private readonly ITraitGroupHandler _traitGroupHandler;
    private readonly ILogger<TraitGroupController> _logger;
    private readonly IValidator<GetTraitGroupByIdQuery> _traitGroupByIdValidator;
    private readonly IValidator<AddTraitGroupRequest> _addValidator;
    private readonly IValidator<UpdateTraitGroupRequest> _updateValidator;
    private readonly IValidator<DeleteTraitGroupRequest> _deleteValidator;


    /// <summary>
    /// Initializes a new instance of the <see cref="TraitGroupController"/> class.
    /// </summary>
    /// <param name="traitGroupHandler">A Trait Group object.</param>
    /// <param name="logger">A logger object for this controller.</param>
    /// <param name="traitGroupByIdValidator">An IValidator object for get by id requests.</param>
    /// <param name="addValidator">An IValidator object for add requests.</param>
    /// <param name="updateValidator">An IValidator object for update requests.</param>
    /// <param name="deleteValidator">An IValidator object for delete requests.</param>
    public TraitGroupController(
        ITraitGroupHandler traitGroupHandler,
        ILogger<TraitGroupController> logger,
        IValidator<GetTraitGroupByIdQuery> traitGroupByIdValidator,
        IValidator<AddTraitGroupRequest> addValidator,
        IValidator<UpdateTraitGroupRequest> updateValidator,
        IValidator<DeleteTraitGroupRequest> deleteValidator)
    {
        _traitGroupHandler = traitGroupHandler;
        _logger = logger;
        _traitGroupByIdValidator = traitGroupByIdValidator;
        _addValidator = addValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    /// <summary>
    ///  Get Trait Group by Id.
    /// </summary>
    /// <param name="request">A request object inferred from the query in the URL.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet("{Id}")]
    public async Task<ActionResult<TraitGroupDto>> GetById(
        [FromRoute] GetTraitGroupByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _traitGroupByIdValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _traitGroupHandler.GetTraitGroupById(request, cancellationToken);
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
    ///  Add Trait Group.
    /// </summary>
    /// <param name="request">A request object inferred from the body.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPost("add")]
    public async Task<ActionResult<TraitGroupDto>> Add(
        [FromBody] AddTraitGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _addValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        try
        {
            var result = await _traitGroupHandler.AddTraitGroup(request, cancellationToken);
            return CreatedAtAction("Add", result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }

    /// <summary>
    ///  Update Trait group.
    /// </summary>
    /// <param name="request">A request object inferred from the body.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpPut("edit")]
    public async Task<IActionResult> Update(
       [FromBody] UpdateTraitGroupRequest request,
       CancellationToken cancellationToken = default)
    {
        var validateResult = await _updateValidator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }
        try
        {
            await _traitGroupHandler.UpdateTraitGroup(request, cancellationToken);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message);
        }
    }

    /// <summary>
    ///  Delete Trait Group.
    /// </summary>
    /// <param name="request">A request object inferred from the query.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(
        [FromQuery] DeleteTraitGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        var validateResult = await _deleteValidator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }
        try
        {
            await _traitGroupHandler.DeleteTraitGroup(request, cancellationToken);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return new ObjectResult(e.Message) { StatusCode = 500 };
        }
    }
}