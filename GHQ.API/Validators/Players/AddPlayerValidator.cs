using FluentValidation;
using GHQ.Core.PlayerLogic.Requests;
using GHQ.Data.Context.Interfaces;

namespace GHQ.API.Validators.Players;

public class AddPlayerValidator : AbstractValidator<AddPlayerRequest>
{
    public AddPlayerValidator(IGHQContext context)
    {
        RuleFor(x => x.UserName).NotEmpty().Must(x => !context.Players.Any(y => y.UserName == x))
          .WithMessage("The UserName you provided already exists in the registry");

        RuleFor(x => x.Email).EmailAddress().MaximumLength(100).WithMessage("Invalid Email").Must(x => !context.Players.Any(y => y.Email == x))
          .WithMessage("The Email address you provided already exists in the registry");
    }
}
