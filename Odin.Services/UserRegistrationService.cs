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
    public class UserSignUpService : IUserSignUpService
    {
        private readonly IUserRepository userRepository;
        private readonly IMailService mailService;
        private readonly IUserTokenRepository userTokenRepository;
        private readonly ISecureHashProvider secureHashProvider;

        public UserSignUpService(IUserRepository userRepository, 
                                 IMailService mailService, 
                                 IUserTokenRepository userTokenRepository,
                                 ISecureHashProvider secureHashProvider)
        {            
            this.userRepository = userRepository;
            this.mailService = mailService;
            this.userTokenRepository = userTokenRepository;
            this.secureHashProvider = secureHashProvider;
        }

        public async Task<UserSignUpResult> SignUp(string userName, string email, string password)
        {
            var result = new UserSignUpResult();
            var existingUser = await userRepository.GetByName(userName);
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
            await userRepository.Create(newUser);

            string tokenString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            tokenString = tokenString.Replace("=", "").Replace("+", "");

            string mailMessage = string.Format("Liebe(r) {0}<br/><br/> Vielen Dank, dass Sie sich für unsere Produkte interessieren und sich registrieren möchten. Klicken Sie einfach zur Bestätigung auf den unten stehenden Link, um Ihre Registrierung abzuschliessen.", userName);
            await mailService.Send("Ihre Registrierungsbestätigung", mailMessage, email);              

           var token = new UserToken()
            {
                Type = UserTokenType.Registration,
                UserName = newUser.Name,
                ExpirationDate = DateTime.Now.AddDays(1),
                Used = false,
                Token = secureHashProvider.Hash(tokenString)
            };                        

            return new UserSignUpResult()
            {
                State = SignUpState.Success,
                User = newUser
            };            
        }

        public async Task<bool> Activate(string token)
        {
            
            
        }
    }
}
