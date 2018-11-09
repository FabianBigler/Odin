using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Core.Model
{
    public class UserActivationResult
    {
        public User User { get; set; }
        public UserActivationState State { get; set; }        
    }

    public enum UserActivationState
    {
        Success,        
        NotValid        
    }
}
