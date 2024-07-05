using GHQ.Data.Context;
using GHQ.Data.Context.Interfaces;
using GHQ.Data.EntityServices.Interfaces;
using GHQ.Data.EntityServices.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GHQ.Data;
public static class DependencyInjection
{
    public static IServiceCollection AddGHQDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GHQContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IGHQContext>(provider => provider.GetService<GHQContext>()!);
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<ICharacterService, CharacterService>();
        services.AddScoped<IRollService, RollService>();
        services.AddScoped<ITraitGroupService, TraitGroupService>();

        // services.AddScoped<IDiceService, DiceService>();

        return services;
    }
}