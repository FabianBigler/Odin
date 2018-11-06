using Odin.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Odin.Core.Interfaces.Services
{
    public interface IUserLoginService
    {
        Task<UserLoginResult> Login(string userName, string password);        
    }
}
