using Odin.Core.Helper;
using Odin.Core.Interfaces.Repositories;
using Odin.Core.Model;
using Odin.Infrastructure.Helpers;
using Odin.Infrastructure.Repositories;
using Odin.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Odin.Infrastructure.Test
{
    public class UserTokenRepositoryTest
    {
        private readonly IUserTokenRepository userTokenRepository;
        private readonly ISecureHashProvider secureHashProvider;
        public UserTokenRepositoryTest()
        {
            userTokenRepository = new UserTokenRepository(TestSettings.ConnectionString);
            secureHashProvider = new SecureHashProvider();
            //userRepository = new UserRepository(TestSettings.ConnectionString);
        }

        [Fact]
        public async Task IntegrationTest()
        {
            try
            {
                string tokenString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                string hashToken = secureHashProvider.Hash(tokenString);
                var tokenId = await userTokenRepository.Create(new UserToken()
                {
                    ExpirationDate = DateTime.Now.AddDays(7),
                    Token = hashToken,
                    Type = UserTokenType.Registration,
                    Used = true,
                    UserId = 1                                       
                });
                
                Assert.True(tokenId != 0);
                var tokens = await userTokenRepository.GetValidTokensByUserId(1);                
                Assert.True(tokens.Count() > 0);

                tokenId = await userTokenRepository.Create(new UserToken()
                {
                    ExpirationDate = DateTime.Now.AddDays(-1),
                    Token = hashToken,
                    Type = UserTokenType.Registration,
                    Used = true,
                    UserId = 2
                });
                tokens = await userTokenRepository.GetValidTokensByUserId(2);
                Assert.True(tokens.Count() == 0);


                //int userId = await userRepository.Create(new User()
                //{
                //    Email = "fabian.bigler@gmail.com",
                //    Name = "Fabian Bigler",
                //    Password = string.Empty,
                //    Deleted = false,
                //    Activated = false,
                //    Company = 0
                //});


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
