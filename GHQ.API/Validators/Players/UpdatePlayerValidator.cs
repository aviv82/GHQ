using GHQ.Core.PlayerLogic.Requests;
using GHQ.Data.Entities;
using FluentValidation;
using GHQ.Data.Context.Interfaces;
using GHQ.Core.Extensions;


namespace GHQ.API.Validators.Players;

public class UpdatePlayerValidator : AbstractValidator<UpdatePlayerRequest>
{
    public UpdatePlayerValidator(IGHQContext context)
    {
        RuleFor(x => x.Id).ValidateExistence<UpdatePlayerRequest, Player>(context).WithMessage("Player not found in registry");

        RuleFor(x => x.Email).MaximumLength(100).NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid Email");

        RuleFor(x => x)
            .Must(x => !context.Players.Any(y => y.Email == x.Email && y.Id != x.Id))
            .WithMessage("The Email address you provided already exists in the registry");

        RuleFor(x => x.UserName).NotEmpty().MaximumLength(100).Must(x => !context.Games.Any(y => y.Title == x))
         .WithMessage("The user name you provided already exists in the registry");
    }
}
