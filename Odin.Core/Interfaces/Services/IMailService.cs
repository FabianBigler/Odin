using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Odin.Core.Interfaces.Services
{
    public interface IMailService
    {
        bool CheckIsValidEmail(string username);
        Task Send(string subject, string body, string to);
    }
}
