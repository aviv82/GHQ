using FluentValidation;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GHQ.Core.Extensions;

public static class AbstractValidatorExtensions
{
    /// <summary>
    /// Adds an asynchronous predicate that checks if the <typeparamref name="TEntity"/> exists in the database.
    /// An <see cref="EntityNotFoundException{TEntity}"/> is thrown when not existent.
    /// </summary>
    /// <typeparam name="T">The type of the object being validated.</typeparam>
    /// <typeparam name="TEntity">The type of the object to check for existence.</typeparam>
    /// <param name="ruleBuilderInitial">The builder to add the validation rule to.</param>
    /// <param name="context">The entity context.</param>
    /// <param name="withCancellation">Indicates whether cancellation should be taken into account.</param>
    /// <returns>RuleBuilderOptions of type T.</returns>
    public static IRuleBuilderOptions<T, int> ValidateExistence<T, TEntity>(
        this IRuleBuilderInitial<T, int> ruleBuilderInitial,
        IGHQContext context,
        bool withCancellation = true)
        where TEntity : BaseEntity
    {
        if (ruleBuilderInitial is null) throw new System.ArgumentNullException(nameof(ruleBuilderInitial));
        if (context is null) throw new System.ArgumentNullException(nameof(context));
        return ruleBuilderInitial
            .MustAsync(async (id, cancellationToken) =>
            {
                var cancellationTokenToUse = withCancellation ? cancellationToken : default;
                var isExistent = await context.GetSet<TEntity>().AnyAsync(x => x.Id == id, cancellationTokenToUse);

                return isExistent;
            }).WithMessage($"Given {typeof(TEntity).Name} does not exist");
    }
}
