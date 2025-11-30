using Microsoft.Extensions.DependencyInjection;

namespace Contacts.Repositories
{
    public static class RepositoryServices
    {
        public static IServiceCollection AddRepoServices(this IServiceCollection services)
        {
            services.AddScoped<Contacts.Services.Contacts.IContactRepository, Contacts.Services.Contacts.ContactRepository>();
            return services;
        }
    }
}
