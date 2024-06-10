using FluentValidation;
using GHQ.Core.CharacterLogic.Handlers;
using GHQ.Core.CharacterLogic.Handlers.Interfaces;
using GHQ.Core.GameLogic.Handlers;
using GHQ.Core.GameLogic.Handlers.Interfaces;
using GHQ.Core.PlayerLogic.Handlers;
using GHQ.Core.PlayerLogic.Handlers.Interfaces;
using GHQ.Core.RollLogic.Handlers;
using GHQ.Core.RollLogic.Handlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GHQ.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        var applicationAssembly = typeof(DependencyInjection).Assembly;

        services
             .AddScoped<IPlayerHandler, PlayerHandler>()
             .AddScoped<IGameHandler, GameHandler>()
             .AddScoped<ICharacterHandler, CharacterHandler>()
             .AddScoped<IRollHandler, RollHandler>()
             .AddValidatorsFromAssembly(applicationAssembly);

        return services;
    }
    public static void RegisterDatabaseService(IServiceCollection services, string connectionString)
    {
        Data.DependencyInjection.AddGHQDataAccess(services, connectionString);
    }
}