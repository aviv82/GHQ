using FluentValidation;
using GHQ.Core;
using System.Reflection;

namespace GHQ.API;

public static class DependencyInjection
{
    public static IServiceCollection AddMonitorAPI(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(Core.DependencyInjection).Assembly;

        services.AddCors();
        services.AddControllers();

        services
        .AddCoreServices()
        .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly)
        .AddLogging();

        services.AddAutoMapper(Assembly.GetExecutingAssembly(), applicationAssembly);
        return services;
    }
}
