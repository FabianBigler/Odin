using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Core.Model
{
    public class UserToken : BaseEntity
    {
        public int UserId { get; set; }
        public UserTokenType Type { get; set;}
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Used { get; set; }
    }


    public enum UserTokenType
    {
        Registration,
        PasswordReset
    }
}
