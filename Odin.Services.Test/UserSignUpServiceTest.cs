using Microsoft.Extensions.DependencyInjection;
using Odin.Core.Interfaces;
using Odin.Core.Interfaces.Repositories;
using Odin.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Odin.Services.Test
{
    public class UserSignUpServiceTest
    {
        private readonly IUserSignUpService userSignUpService;
        public UserSignUpServiceTest()
        {                        
            //var userRepository = new UserRepository("data source=PUMBA01\\SQLEXPRESS;initial catalog = Odin; persist security info=True; Integrated Security = SSPI;");
            //var mailService = new MailService()


            //userSignUpService = new UserSignUpService(userRepository, 


        }

        [Fact]
        public void SignUpTest()
        {
            
        }
    }
}
