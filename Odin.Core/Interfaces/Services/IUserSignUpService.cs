using Odin.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Odin.Core.Interfaces
{
    public interface IUserSignUpService
    {
        Task<UserSignUpResult> SignUp(string username, string email, string password);        
    }
}
