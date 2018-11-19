using Microsoft.Extensions.DependencyInjection;
using Odin.Core.Helper;
using Odin.Core.Interfaces;
using Odin.Core.Interfaces.Repositories;
using Odin.Core.Interfaces.Services;
using Odin.Core.Model;
using Odin.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Services
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddOdinServices(this IServiceCollection services, SmtpConfig smtpConfig, string baseUrl)
        {
            services.AddTransient<ISecureHashProvider, SecureHashProvider>();            
            services.AddTransient<IMailService, MailService>(_ =>
            {
                return new MailService(smtpConfig);
            });
            services.AddTransient<IUserLoginService, UserLoginService>();        
            services.AddTransient<IUserSignUpService, UserSignUpService>();

            services.AddTransient<IUserSignUpService, UserSignUpService>(sp =>
                                new UserSignUpService(sp.GetService<IUserRepository>(), sp.GetService<IMailService>(),
                                                      sp.GetService<IUserTokenRepository>(), sp.GetService<ISecureHashProvider>(), baseUrl));            
            return services;
        }
    }
}
