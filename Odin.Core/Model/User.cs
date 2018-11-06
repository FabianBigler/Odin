using Odin.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Core.Model
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get;  set; }        
        public DateTime CreatedOn { get; set; }       
        public bool Deleted { get; set; }
        public bool Activated { get; set; }
    }
}
