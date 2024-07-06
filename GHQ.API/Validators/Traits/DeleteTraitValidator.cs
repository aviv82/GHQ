using FluentValidation;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using GHQ.Core.Extensions;
using GHQ.Core.TraitLogic.Requests;

namespace GHQ.API.Validators.Traits;
public class DeleteTraitValidator : AbstractValidator<DeleteTraitRequest>
{
    public DeleteTraitValidator(IGHQContext context)
    {
        RuleFor(x => x.Id)
        .ValidateExistence<DeleteTraitRequest, Trait>(context)
        .WithMessage("Trait does not exist."); ;
    }
}