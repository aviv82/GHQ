using FluentValidation;
using GHQ.Core.Extensions;
using GHQ.Core.RollLogic.Requests;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Rolls;

public class DeleteRollValidator : AbstractValidator<DeleteRollRequest>
{
    public DeleteRollValidator(IGHQContext context)
    {
        RuleFor(x => x.Id).ValidateExistence<DeleteRollRequest, Roll>(context).WithMessage("Roll does not exist."); ;
    }
}
