using System.Reflection;
using Example02.Application.Behaviours;
using FluentValidation;

namespace Example02.Application;

public static class DependencyInjection
{
    private static readonly Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(CurrentAssembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(CurrentAssembly);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });
        return services;
    }
}