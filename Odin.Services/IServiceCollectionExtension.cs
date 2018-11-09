using Microsoft.Extensions.DependencyInjection;
using Odin.Core.Interfaces;
using Odin.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddOdinServices(this IServiceCollection services)
        {
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IUserLoginService, UserLoginService>();
            services.AddTransient<IUserSignUpService, UserSignUpService>();            
            return services;
        }
    }
}
