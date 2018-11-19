using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odin.Web.Model
{
    public class UserSignUpArgs
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
