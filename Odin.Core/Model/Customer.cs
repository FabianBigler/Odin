using Odin.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Odin.Core.Model
{
    public class Customer : BaseEntity, ITrackingUserChange
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set;}
        public string Country { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
    }
}
