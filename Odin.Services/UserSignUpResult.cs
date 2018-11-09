using Odin.Core.Helper;
using Odin.Core.Interfaces;
using Odin.Core.Interfaces.Repositories;
using Odin.Core.Interfaces.Services;
using Odin.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odin.Services
{
    public class UserSignUpService : IUserSignUpService
    {
        private readonly IUserRepository userRepository;
        private readonly IMailService mailService;
        private readonly IUserTokenRepository userTokenRepository;
        private readonly ISecureHashProvider secureHashProvider;
        private readonly string baseUrl;

        public UserSignUpService(IUserRepository userRepository, 
                                 IMailService mailService, 
                                 IUserTokenRepository userTokenRepository,
                                 ISecureHashProvider secureHashProvider,
                                 string baseUrl)
        {            
            this.userRepository = userRepository;
            this.mailService = mailService;
            this.userTokenRepository = userTokenRepository;
            this.secureHashProvider = secureHashProvider;
            this.baseUrl = baseUrl;
        }

        public async Task<UserSignUpResult> SignUp(string userName, string email, string password)
        {
            var result = new UserSignUpResult();
            var existingUser = await userRepository.GetByNameOrEmail(userName);
            if(existingUser != null)
            {                
                return new UserSignUpResult()
                {
                    State = SignUpState.UserAlreadyExists
                };
            }

            if(!mailService.CheckIsValidEmail(email))
            {
                return new UserSignUpResult()
                {
                    State = SignUpState.EmailNotValid
                };
            }

            var newUser = new User()
            {
                Email = email,
                Name = userName
            };
            int userId = await userRepository.Create(newUser);

            string tokenString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            tokenString = tokenString.Replace("=", "").Replace("+", "");
            string tokenUrl = Path.Combine(baseUrl, string.Format("/signup/activate?token={0}", tokenString));
            //TODO @ FB: Internationalization
            string mailMessage = string.Format("Liebe(r) {0}<br/><br/> Vielen Dank, dass Sie sich für unsere Produkte interessieren und sich registrieren möchten. Klicken Sie einfach zur Bestätigung auf den unten stehenden Link, um Ihre Registrierung abzuschliessen. <br/><br/> {1}", userName, tokenString);
            await mailService.Send("Ihre Registrierungsbestätigung", mailMessage, email);              

           var token = new UserToken()
            {
                Type = UserTokenType.Registration,                
                UserId = userId,
                ExpirationDate = DateTime.Now.AddDays(1),
                Used = false,
                Token = secureHashProvider.Hash(tokenString)
            };                
            await userTokenRepository.Create(token);

            return new UserSignUpResult()
            {
                State = SignUpState.Success,
                User = newUser
            };            
        }
        
        public async Task<UserActivationResult> Activate(int userId, string token)
        {
            var userTokens = await userTokenRepository.GetAllByUserId(userId);
            if(userTokens.Count() == 0)
            {
                return new UserActivationResult() 
                {
                    State = UserActivationState.NotValid
                };
            }

            foreach(var userToken in userTokens)
            {
                if (secureHashProvider.Verify(token, userToken.Token))
                {
                    userToken.Used = true;
                    await userTokenRepository.Update(userToken);
                    var user = await userRepository.GetById(userId);
                    return new UserActivationResult()
                    {
                        State = UserActivationState.Success,                        
                        User = user
                    };
                }
            }

            return new UserActivationResult()
            {
                State = UserActivationState.NotValid
            };
        }
    }
}
