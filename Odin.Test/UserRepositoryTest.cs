using Odin.Core.Interfaces.Repositories;
using Odin.Core.Model;
using Odin.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Odin.Test
{    
    public class UserRepositoryTest
    {
        private readonly IUserRepository userRepository;
        public UserRepositoryTest()
        {
            userRepository = new UserRepository(TestSettings.ConnectionString);
        }

        [Fact]
        public async Task WalkThroughTest()
        {
            try
            {
                int userId = await userRepository.Create(new User()
                {
                    Email = "fabian.bigler@gmail.com",
                    Name = "Fabian Bigler",
                    Password = string.Empty,
                    Deleted = false,
                    Activated = false,
                    Company = 0                  
                });
                
                var user = await userRepository.GetByNameOrEmail("Fabian Bigler");
                Assert.Equal("Fabian Bigler", user.Name);
                user = await userRepository.GetById(userId);
                Assert.Equal("Fabian Bigler", user.Name);
                await userRepository.Delete(user);
                user = await userRepository.GetByNameOrEmail("Fabian Bigler");
                Assert.Null(user);
            }
            catch (Exception ex)
            {
                throw;
            }
         
        }
    }
}
