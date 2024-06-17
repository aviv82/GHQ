using FluentValidation;
using GHQ.Core.Extensions;
using GHQ.Core.RollLogic.Queries;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Rolls;
public class GetRollByIdValidator : AbstractValidator<GetRollByIdQuery>
{
    public GetRollByIdValidator(IGHQContext context)
    {
        RuleFor(x => x.Id).ValidateExistence<GetRollByIdQuery, Roll>(context);
    }
}
