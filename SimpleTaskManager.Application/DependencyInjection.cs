using Microsoft.Extensions.DependencyInjection;

using SimpleTaskManager.Application.UseCases.Tasks.Register;

namespace SimpleTaskManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterTaskUseCase>();
        
        return services;
    }
}