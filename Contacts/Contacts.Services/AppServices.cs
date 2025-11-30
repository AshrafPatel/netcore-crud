using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Contacts.Services.Contacts;
using Contacts.Services.Profiles;

namespace Contacts.Services
{
    public static class AppServices
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => {
                cfg.AddProfile<MapperConfig>();
            });

            services.AddScoped<IContactService, ContactService>();
            return services;
        }
    }
}
