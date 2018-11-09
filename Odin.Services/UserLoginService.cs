using Odin.Core.Helper;
using Odin.Core.Interfaces;
using Odin.Core.Interfaces.Repositories;
using Odin.Core.Interfaces.Services;
using Odin.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Odin.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserRepository userRepository;
        private readonly ISecureHashProvider secureHashProvider;

        public UserLoginService(IUserRepository userRepository, ISecureHashProvider secureHashProvider)
        {
            this.userRepository = userRepository;
            this.secureHashProvider = secureHashProvider;
        }

        public async Task<UserLoginResult> Login(string input, string password)
        {
            var result = new UserLoginResult();

            var user = await userRepository.GetByNameOrEmail(input);
            if(user == null)
            {
                result.State = UserLoginState.UserNotFound;
                return result;
            }

            if(secureHashProvider.Verify(password, user.Password))
            {
                result.State = UserLoginState.Success;                
                result.User = user;
            } else
            {
                result.State = UserLoginState.WrongPassword;                
            }

            return result;
        }
    }
}
