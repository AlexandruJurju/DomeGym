using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DomeGym.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR();
        return services;
    }


    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(options => { options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)); });
        return services;
    }
}