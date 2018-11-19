using Microsoft.Extensions.DependencyInjection;
using Odin.Core.Interfaces.Repositories;
using Odin.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddOdinRepositories(this IServiceCollection services, string connectionString)
        {            

            services.AddTransient<IUserRepository, UserRepository>(_ =>
            {                
                return new UserRepository(connectionString);
            });

            services.AddTransient<IUserTokenRepository, UserTokenRepository>(_ =>
            {
                return new UserTokenRepository(connectionString);
            });
        
            return services;
        }
    }
}
