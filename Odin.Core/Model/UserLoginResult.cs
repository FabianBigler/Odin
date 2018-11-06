using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Core.Model
{
    public class UserLoginResult
    {
        public UserLoginState State { get; set; }
        public User User { get; set; }
    }

    public enum UserLoginState
    {
        Success,
        UserNotFound,
        WrongPassword
    }
}
