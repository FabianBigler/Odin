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
        public static IServiceCollection AddOdinRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserTokenRepository, UserTokenRepository>();            
            return services;
        }
    }
}
