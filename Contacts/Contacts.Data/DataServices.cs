using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Contacts.Data
{
    public static class DataServices
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ContactsDb");
            builder.Services.AddDbContext<ContactDbContext>(options => options.UseSqlServer(connectionString));

            // Register repositories
            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }
    }
}
