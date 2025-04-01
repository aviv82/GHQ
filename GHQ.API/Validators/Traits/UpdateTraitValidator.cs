using FluentValidation;
using GHQ.Core.Extensions;
using GHQ.Core.TraitLogic.Requests;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;

namespace GHQ.API.Validators.Traits;
public class UpdateTraitValidator : AbstractValidator<UpdateTraitRequest>
{
    public UpdateTraitValidator(IGHQContext context)
    {
        RuleFor(x => x.Id)
        .ValidateExistence<UpdateTraitRequest, Trait>(context)
        .WithMessage("Trait Id does not appear in the records");

        RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithMessage("Invalid Trait Title");
    }
}