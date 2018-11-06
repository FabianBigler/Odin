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
        public async Task OverallTest()
        {
            await userRepository.Create(new User()
            {
                Email = "fabian.bigler@gmail.com",
                Name = "Fabian Bigler"
            });
            var user = await userRepository.GetByName("Fabian Bigler");
            Assert.Equal("Fabian Bigler", user.Name);
            await userRepository.Delete(user);

            user = await userRepository.GetByName("Fabian Bigler");
            Assert.Null(user);
        }
    }
}
