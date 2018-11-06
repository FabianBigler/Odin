using Odin.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Odin.Core.Interfaces.Repositories
{
    public interface IUserTokenRepository : IRepository<UserToken>
    {
        Task<IEnumerable<UserToken>> GetAllByName(string name);
    }
}
