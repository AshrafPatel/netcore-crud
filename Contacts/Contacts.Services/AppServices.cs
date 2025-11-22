using Microsoft.Extensions.DependencyInjection;

namespace Contacts.Services
{
    public static class AppServices
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            // Register service-layer types
            services.AddScoped<ITodoService, TodoService>();

            // Register other services here, e.g.:
            // services.AddScoped<IOtherService, OtherService>();

            return services;
        }
    }
}
