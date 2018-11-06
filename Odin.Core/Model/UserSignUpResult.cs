using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Core.Model
{
    public class UserSignUpResult
    {
        public SignUpState State { get; set; }
        public User User { get; set; }
    }

    public enum SignUpState
    {
        Success,
        UserAlreadyExists,        
        EmailNotValid        
    }
}
